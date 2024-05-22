using Xunit;
using Microsoft.AspNetCore.Mvc;
using IFAB.Controllers;
using IFAB.Models;
using System.Collections.Generic;

namespace IFAB.Tests
{
    public class QuizControllerTests
    {
        [Fact]
        // test that the Index action returns a view result with a list of questions objects
        public void Index_ReturnsAViewResult_WithAListOfQuestions()
        {
            //Arange
            var controller = new QuizController();

            //Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Questions>>(viewResult.ViewData.Model);    
            Assert.Equal(3, model.Count);
        }

        //[Fact]

        //public void GetQuestion_ReturnsAQuestion_ForValidID()
        //{
        //    // Arrange 
        //    var controller = new QuizController();

        //    // Act
        //    var result = controller.GetQuestion();

        //    // Assert
        //    var ViewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<Questions>(ViewResult.ViewData.Model);
        //    Assert.Equal(1, model.Id);
        //}

        

    }
}