using Microsoft.AspNetCore.Mvc;
using Moq;
using MyFirstBlog.Controllers;
using MyFirstBlog.Dtos;
using MyFirstBlog.Services;
using System;
using NUnit.Framework;

namespace MyFirstBlogTests.Controllers
{
    public class PostsControllerTests
    {
        [Test]
        public void IfSlugExists()
        {
            // Step 1: Create a fake post service
            var fakePostService = new Mock<IPostService>();

            // Step 2: Create a fake post to return
            var fakePost = new PostDto
            {
                Title = "Testing My Blog Post ",
                Slug = "my-blog-post"
            };

            // Step 3: Tell the fake service what to return when called
            fakePostService.Setup(service => service.GetPost("my-blog-post"))
                          .Returns(fakePost);

            // Step 4: Create the controller with our fake service
            var controller = new PostsController(fakePostService.Object);

            // Step 5: Call the method we want to test
            var result = controller.GetPost("my-blog-post");

            // Step 6: Check if we got the right answer
            Assert.Equals("Testing My Blog Post ", result.Value.Title);
        }

        [Test]
        public void IfSlugDoesNotExist()
        {
            // Create a fake service that returns nothing
            var mockPostService = new Mock<IPostService>();
            mockPostService.Setup(service => service.GetPost("fake-slug"))
                          .Returns((PostDto)null);

            // Create controller
            var controller = new PostsController(mockPostService.Object);

            // Call the method
            var result = controller.GetPost("fake-slug");

            // Check that we got "Not Found"
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
