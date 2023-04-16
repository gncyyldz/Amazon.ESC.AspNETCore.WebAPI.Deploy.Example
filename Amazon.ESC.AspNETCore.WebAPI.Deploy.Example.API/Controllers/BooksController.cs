using Amazon.ESC.AspNETCore.WebAPI.Deploy.Example.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Amazon.ESC.AspNETCore.WebAPI.Deploy.Example.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        readonly IMongoCollection<Book> _bookCollection;
        public BooksController(IConfiguration configuration)
        {
            MongoClient mongoClient = new(configuration["MongoDBConfigurations:ConnectionString"]);
            IMongoDatabase database = mongoClient.GetDatabase(configuration["MongoDBConfigurations:Database"]);
            _bookCollection = database.GetCollection<Book>(configuration["MongoDBConfigurations:BookCollection"]);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _bookCollection.Find(b => true)
                                    .ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            await _bookCollection.InsertOneAsync(book);
            return Ok(book.Id);
        }
    }
}
