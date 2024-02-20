using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SequencesWebApp.Controllers;
using SequencesWebApp.Interfaces;
using SequencesWebApp.Models;
using SequencesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencesWebApp.Tests.Controller
{
    
    public class SequenceControllerTests
    {
        private ISequenceRepository _sequenceRepository;
        private SequenceController _sequenceController;
        public SequenceControllerTests()
        {
            // Dependencies
            _sequenceRepository = A.Fake<ISequenceRepository>();

            // SUT
            var tempData = new TempDataDictionary(new DefaultHttpContext(), A.Fake<ITempDataProvider>());
            _sequenceController = new SequenceController(_sequenceRepository)
            {
                TempData = tempData // Initialise the TempData property
            };
        }

        [Fact]
        public async void SequenceController_Index_ReturnsSuccess()
        {
            // Arrange
            var sequences = A.Fake<List<Sequence>>();
            A.CallTo(() => _sequenceRepository.GetAllAsync()).Returns(sequences);

            // Act
            var result = await _sequenceController.Index();

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.ViewData.Model;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(HomeViewModel));
        }

        [Fact]
        public async void SequenceController_Detail_ReturnsSuccess()
        {
            // Arrange
            var id = 1;
            var integers = A.Fake<List<SequenceInt>>();
            Sequence? sequence = new Sequence
            {
                Id = id,
                Integers = integers,
                IsAscending = true,
                SortingTime = 0.001f
            };
            A.CallTo(() => _sequenceRepository.GetByIdAsync(id)).Returns(Task.FromResult<Sequence?>(sequence));

            // Act
            var result = await _sequenceController.Detail(id);

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.ViewData.Model;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(SequenceDetailViewModel));
        }

        [Fact]
        public void SequenceController_Create_ReturnsView()
        {
            // Arrange


            // Act
            var result =  _sequenceController.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void SequenceController_Create_POST_ReturnsOk()
        {
            // Arrange
            var viewModel = new SequenceCreateViewModel
            {
                Sequence = new List<int> { 1, 2 },
                IsAscending = true,
                SortingTime = 0.1f
            };
            string expectedMessage = "Sequence saved successfully.";
            string key = "SaveSuccessMessage";

            // Act
            var result = _sequenceController.Create(viewModel);

            // Assert
            result.Should().BeOfType<OkResult>();
            var tempData = _sequenceController.TempData;
            tempData.Should().NotBeNull();
            tempData.ContainsKey(key).Should().BeTrue();
            tempData[key].Should().Be(expectedMessage);
        }

        [Fact]
        public void SequenceController_Create_POST_ReturnsBadRequest()
        {
            // Arrange
            var viewModel = new SequenceCreateViewModel();

            // Act
            var result = _sequenceController.Create(viewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }



        [Fact]
        public async void SequenceController_JSON_ReturnsContent()
        {
            // Arrange
            string s = "json";
            A.CallTo(() => _sequenceRepository.GetAllAsJsonAsync()).Returns(s);
            string expectedContentType = "application/json";

            // Retrieve JSON string from repository
            string jsonData = await _sequenceRepository.GetAllAsJsonAsync();
            // Show on page


            // Act
            var result = await _sequenceController.JSON();

            // Assert
            result.Should().BeOfType<ContentResult>();
            result.As<ContentResult>().ContentType.Should().Be(expectedContentType);
        }



        [Fact]
        public async void SequenceController_DownloadJsonFile_ReturnsFile()
        {
            // Arrange
            string s = "json";
            A.CallTo(() => _sequenceRepository.GetAllAsJsonAsync()).Returns(s);
            string expectedFileName = "sequences.json";
            string expectedContentType = "application/json";

            // Act
            var result = await _sequenceController.DownloadJsonFile();

            // Assert
            result.Should().BeOfType<FileContentResult>();
            result.As<FileContentResult>().ContentType.Should().Be(expectedContentType);
            result.Should().BeOfType<FileContentResult>()
                .Which.FileDownloadName.Should().Be(expectedFileName);
        }


        [Fact]
        public async void SequenceController_Delete_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var integers = A.Fake<List<SequenceInt>>();
            Sequence? sequence = new Sequence
            {
                Id = id,
                Integers = integers,
                IsAscending = true,
                SortingTime = 0.001f
            };
            A.CallTo(() => _sequenceRepository.GetByIdAsync(id)).Returns(Task.FromResult<Sequence?>(sequence));

            // Act
            var result = await _sequenceController.Delete(id);

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>().Subject;
            var model = viewResult.ViewData.Model;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(Sequence));
        }

        [Fact]
        public async void SequenceController_DeleteConfirmed_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var integers = A.Fake<List<SequenceInt>>();
            string expectedRedirectName = "Index";
            string expectedMessage = "Sequence deleted successfully.";
            string key = "DeleteSuccessMessage";
            Sequence? sequence = new Sequence
            {
                Id = id,
                Integers = integers,
                IsAscending = true,
                SortingTime = 0.001f
            };
            A.CallTo(() => _sequenceRepository.GetByIdAsync(id)).Returns(Task.FromResult<Sequence?>(sequence));

            // Act
            var result = await _sequenceController.DeleteConfirmed(id);

            // Assert
            var redirectResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
            redirectResult.ActionName.Should().Be(expectedRedirectName);

            var tempData = _sequenceController.TempData;
            tempData.Should().NotBeNull();
            tempData.ContainsKey(key).Should().BeTrue();
            tempData[key].Should().Be(expectedMessage);
        }
    }
}
