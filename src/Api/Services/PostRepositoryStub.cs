using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Example.Api.Domain;

namespace Example.Api.Services
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync(CancellationToken token);

        Task<Post> CreatePostAsync(string title, CancellationToken token);

        Task<Post> UpdatePostAsync(Post modifiedPost, CancellationToken token);

        Task<bool> DeletePostAsync(Guid id, CancellationToken token);
    }

    public class PostRepositoryStub : IPostRepository
    {
        private List<Post> posts;

        public PostRepositoryStub()
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

        public async Task<List<Post>> GetAllPostsAsync(CancellationToken token)
        {
            return await Task.FromResult(posts);
        }

        public async Task<Post> CreatePostAsync(string title, CancellationToken token)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = title
            };

            posts.Add(post);

            return await Task.FromResult(post);
        }

        public async Task<Post> UpdatePostAsync(Post modifiedPost, CancellationToken token)
        {
            var index = posts.FindIndex(x => x.Id == modifiedPost.Id);
            var postNotFound = index == -1;

            if (postNotFound)
            {
                return await Task.FromResult(default(Post));
            }

            posts[index] = modifiedPost;
            return await Task.FromResult(modifiedPost);
        }

        public async Task<bool> DeletePostAsync(Guid id, CancellationToken token)
        {
            var index = posts.FindIndex(x => x.Id == id);
            var postNotFound = index == -1;

            if (postNotFound)
            {
                return await Task.FromResult(false);
            }

            posts.RemoveAt(index);

            return await Task.FromResult(true);
        }
    }
}