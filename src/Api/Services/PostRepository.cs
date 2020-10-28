using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Api.Domain;

namespace Example.Api.Services
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync();

        Task<Post> CreatePostAsync(string title);

        Task<bool> DeletePostAsync(Guid id);
    }

    public class PostRepository : IPostRepository
    {
        private List<Post> posts;

        public PostRepository()
        {
            posts = new List<Post>();
            for(int i=0; i<5; i++)
            {
                posts.Add(new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = $"random title number: {i}"
                });
            }
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await Task.FromResult(posts);
        }

        public async Task<Post> CreatePostAsync(string title)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = title
            };

            posts.Add(post);

            return await Task.FromResult(post);
        }

        public async Task<bool> DeletePostAsync(Guid id)
        {
            bool removedSuccessfully = posts.Remove(posts.Find(post => post.Id == id));

            return await Task.FromResult(removedSuccessfully);
        }
    }
}