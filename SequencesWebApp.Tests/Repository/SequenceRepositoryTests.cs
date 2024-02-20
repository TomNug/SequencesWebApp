using FakeItEasy;
using FluentAssertions;
using ListsWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using SequencesWebApp.Controllers;
using SequencesWebApp.Interfaces;
using SequencesWebApp.Models;
using SequencesWebApp.Repositories;
using SequencesWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencesWebApp.Tests.Repository
{
    public class SequenceRepositoryTests
    {
        private ISequenceRepository _sequenceRepository;
        private ApplicationDbContext _context;

        public SequenceRepositoryTests()
        {
            _context = GetDbContext();

            // SUT
            _sequenceRepository = new SequenceRepository(_context);
        }

        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            // Adds entities to the database

            if (databaseContext.Sequences.Count() == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    // Create list of SequenceInts
                    // (the integers which form the sequence)
                    var integers = new List<SequenceInt>();

                    for (int j = 0; j < 4; j++)
                    {
                        var sequenceInt = new SequenceInt()
                        {
                            Value = i + j,
                        };
                        integers.Add(sequenceInt);
                    }

                    databaseContext.Sequences.Add(
                        new Sequence()
                        {
                            Integers = integers,
                            IsAscending = true,
                            SortingTime = i
                        });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void SequenceRepository_Add_ReturnsBool()
        {
            // Arrange
            var integers = new List<SequenceInt>();

            for (int i = 0; i < 4; i++)
            {
                var sequenceInt = new SequenceInt()
                {
                    Value = i,
                };
                integers.Add(sequenceInt);
            }

            var sequence = new Sequence()
            {
                Integers = integers,
                IsAscending = true,
                SortingTime = 0.5f
            };

            // Act
            var result = _sequenceRepository.Add(sequence);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void SequenceRepository_GetByIdAsync_ReturnsSequence()
        {
            // Arrange
            var id = 1;
            // Act
            var result = await _sequenceRepository.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Sequence>();
        }

        [Fact]
        public async void SequenceRepository_GetByIdAsync_ReturnsNull()
        {
            // Arrange
            var id = -1;
            // Act
            var result = await _sequenceRepository.GetByIdAsync(id);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void SequenceRepository_GetAllAsync_ReturnsList()
        {
            // Arrange

            // Act
            var result = await _sequenceRepository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Sequence>>();
            result.Should().HaveCount(10);
        }


        [Fact]
        public async void SequenceRepository_GetAllAsJsonAsync_ReturnsString()
        {
            // Arrange

            // Act
            var result = await _sequenceRepository.GetAllAsJsonAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
        }

        [Fact]
        public void SequenceRepository_Delete_ReturnsTrue()
        {
            // Arrange
            var sequence = _context.Sequences.FirstOrDefault();

            // Act
            var result = _sequenceRepository.Delete(sequence);

            // Assert
            result.Should().BeTrue();
            _context.Sequences.Should().NotContain(sequence);
        }

        [Fact]
        public void SequenceRepository_Delete_ReturnsFalse()
        {
            // Arrange
            var sequence = new Sequence { Id = 1000 }; // Does not exist

            // Act
            bool result;
            try
            {
                result = _sequenceRepository.Delete(sequence);
            }
            catch (DbUpdateConcurrencyException)
            {
                result = false;
            }
            // Assert
            result.Should().BeFalse();
        }

    }
}

