using Microsoft.AspNetCore.Mvc;
using Moq;
using MyFirstBlog.Controllers;
using MyFirstBlog.Dtos;
using MyFirstBlog.Services;
using System;
using Xunit;

namespace MyFirstBlogTests.Controllers
{
    public class PostsControllerTests
    {
        [Fact]
        public void GetPost_ReturnsPost_WhenSlugExists()
        {
            // Step 1: Create a fake post service
            var fakePostService = new Mock<IPostService>();

            // Step 2: Create a fake post to return
            var fakePost = new PostDto
            {
                Title = "My Blog Post Test",
                Slug = "my-blog-post-test"
            };

            // Step 3: Tell the fake service what to return when called
            fakePostService.Setup(service => service.GetPost("my-blog-post"))
                          .Returns(fakePost);

            // Step 4: Create the controller with our fake service
            var controller = new PostsController(fakePostService.Object);

            // Step 5: Call the method we want to test
            var result = controller.GetPost("my-blog-post");

            // Step 6: Check if we got the right answer
            Assert.Equal("My Blog Post", result.Value.Title);
        }

        [Fact]
        public void GetPost_ReturnsNotFound_WhenSlugDoesNotExist()
        {
            // Create a fake service that returns nothing
            var mockPostService = new Mock<IPostService>();
            mockPostService.Setup(service => service.GetPost("fake-slug"))
                          .Returns((PostDto)null);

            // Create controller
            var controller = new PostsController(mockPostService.Object);

            // Call the method
            var result = controller.GetPost("fake-slug");

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}