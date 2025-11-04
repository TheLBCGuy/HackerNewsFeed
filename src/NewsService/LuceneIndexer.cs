using DataContract;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace NewsService
{
    public class LuceneIndexer : IIndexer, IDisposable
    {
        private readonly RAMDirectory _directory = new();
        private readonly IndexWriter _indexWriter;
        private const int _maxTopDocCount = 1000;
        private const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

        private LuceneIndexer()
        {
            _indexWriter = CreateWriter();
        }

        public static readonly LuceneIndexer Instance = new();

        public IEnumerable<int> Search(string term)
        {
            var query = CreateQuery(term);
            using var indexReader = DirectoryReader.Open(_directory);
            var searcher = new IndexSearcher(indexReader);
            var topDocs = searcher.Search(query, _maxTopDocCount);
            return MapDocIds(searcher, topDocs);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _directory.Dispose();
            _indexWriter.Dispose();
        }

        public void Add(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            var doc = CreateDocument(item);
            _indexWriter.AddDocument(doc);
            _indexWriter.Flush(triggerMerge: false, applyAllDeletes: false);
            _indexWriter.Commit();
        }

        private IndexWriter CreateWriter()
        {
            var analyzer = new StandardAnalyzer(AppLuceneVersion);
            var config = new IndexWriterConfig(AppLuceneVersion, analyzer)
            {
                OpenMode = OpenMode.CREATE
            };
            return new IndexWriter(_directory, config);
        }

        private static Document CreateDocument(Item item)
        {
            var doc = new Document {
                new Int32Field(nameof(item.Id), item.Id, Field.Store.YES),
                new StringField(nameof(item.Title), (item.Title ?? string.Empty).ToLowerInvariant(), Field.Store.YES),
                new StringField(nameof(item.Text), (item.Text ?? string.Empty).ToLowerInvariant(), Field.Store.YES)
            };
            return doc;
        }

        private static BooleanQuery CreateQuery(string term)
        {
            var wildCardTerm = $"*{term.ToLowerInvariant()}*";
            var titleTerm = new WildcardQuery(new Term(nameof(Item.Title), wildCardTerm));
            var textTerm = new WildcardQuery(new Term(nameof(Item.Text), wildCardTerm));
            var authorTerm = new WildcardQuery(new Term(nameof(Item.By), wildCardTerm));
            return new BooleanQuery{
                { titleTerm, Occur.SHOULD },
                { textTerm, Occur.SHOULD },
                { authorTerm, Occur.SHOULD }
            };
        }

        private static List<int> MapDocIds(IndexSearcher searcher, TopDocs topDocs)
        {
            var ids = new List<int>(topDocs.TotalHits);
            for (var i = 0; i < topDocs.ScoreDocs.Length; i++)
            {
                var doc = searcher.Doc(topDocs.ScoreDocs[i].Doc);
                var docId = doc.GetField(nameof(Item.Id)).GetInt32Value();
                if (!docId.HasValue) continue;
                ids.Add(docId.Value);
            }
            return ids;
        }
    }
}
