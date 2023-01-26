﻿using ClinicService.Controllers;
using ClinicService.Models;
using ClinicService.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicServiceTests
{
    /// <summary>
    /// ДОМАШНЯЯ РАБОТА: разработать методы тестирования контроллера PetController:
    /// Обновление данных UpdateTest
    /// Получение данных (по всем животным) GetAllTest
    /// Получение данных (по конкретному животному) GetByIdTest
    /// </summary>
    public class PetControllerTests
    {

        private PetController _petController;

        public PetControllerTests()
        {
            _petController = new PetController();
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void PetGetByIdBadRequest(int id)
        {
            ActionResult<Pet> operationResult = _petController.GetById(id);

            Assert.IsType<BadRequestObjectResult>(operationResult.Result);
            Assert.Equal<int>(0, (int)((BadRequestObjectResult)operationResult.Result).Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        public void PetGetById(int id) 
        {
            ActionResult<Pet> operationResult = _petController.GetById(id);

            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<Pet>(((OkObjectResult)operationResult.Result).Value);
        }

        [Fact]
        public void PetGetAllTest()
        {
            ActionResult<List<Pet>> operationResult = _petController.GetAll();
            
            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<List<Pet>>(((OkObjectResult)operationResult.Result).Value);
            
        }

        [Fact]
        public void PetUpdateTest()
        {
            UpdatePetRequest updatePetRequest = new UpdatePetRequest();
            updatePetRequest.PetId = 3;
            updatePetRequest.Name = "Мур";
            updatePetRequest.Birthday = DateTime.Now;

            ActionResult<int> operationResult = _petController.Update(updatePetRequest);
            int expectedOperationResult = 1;

            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.Equal(expectedOperationResult, (int)((OkObjectResult)operationResult.Result).Value);

        }

        [Fact]
        public void PetCreateBadRequestTest()
        {

            CreatePetRequest createPetRequest = new CreatePetRequest();
            createPetRequest.Name = "П";
            createPetRequest.Birthday = DateTime.Now.AddYears(-55);
            createPetRequest.ClientId = 1;

            ActionResult<int> operationResult = _petController.Create(createPetRequest);

            int expectedOperationValue = 0;

            Assert.IsType<BadRequestObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)((BadRequestObjectResult)operationResult.Result).Value);

        }

        [Fact]
        public void PetCreateTest()
        {
            // МЕТОД ТЕСТИРОВАНИЯ СОСТОИТ ИЗ УСЛОВНЫХ 3 ЧАСТЕЙ

            // [1] Подготовка данных для тестирования
            CreatePetRequest createPetRequest = new CreatePetRequest();
            createPetRequest.Name = "Персик";
            createPetRequest.Birthday = DateTime.Now.AddYears(-15);
            createPetRequest.ClientId = 1;

            // [2] Исполнение тестируемого метода
            ActionResult<int> operationResult = _petController.Create(createPetRequest);


            // [3] Подготовка эталонного результата (expected), проверка результата
            int expectedOperationValue = 1;

            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)((OkObjectResult)operationResult.Result).Value);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void DeletePetBadRequestTest(int petId)
        {

            // [2] Исполнение тестируемого метода
            ActionResult<int> operationResult = _petController.Delete(petId);

            // [3] Подготовка эталонного результата (expected), проверка результата
            int expectedOperationValue = 0;

            Assert.IsType<BadRequestObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)((BadRequestObjectResult)operationResult.Result).Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void DeletePetTest(int petId)
        {

            // [2] Исполнение тестируемого метода
            ActionResult<int> operationResult =  _petController.Delete(petId);

            // [3] Подготовка эталонного результата (expected), проверка результата
            int expectedOperationValue = 1;

            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)((OkObjectResult)operationResult.Result).Value);
        }


    }
}
