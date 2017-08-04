using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Zer.Entities;
using Zer.Framework.Entities;
using Zer.GytDto;
using Zer.GytDto.Extensions;

namespace com.gyt.ms.Tests.AppService
{
    [TestFixture]
    public class AutoMapperExtensionsWithDtoTests : TestBase
    {
        [Test]
        public void TestFor_MapToDto_ExpecteSuccess()
        {
            var companyInfo = new CompanyInfo()
            {
                CompanyName = "CompanyName",
                Id = 1,
                State = State.Active,
                TraderRange = "TraderRange"
            };

            var expected = Mapper.Map<CompanyInfoDto>(companyInfo);

            var actual = companyInfo.Map<CompanyInfoDto>();

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void TestFor_MapToDtoOfIEnumerable_ExpecteSuccess()
        {
            var list = new List<CompanyInfo>
            {
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 1,
                    State = State.Active,
                    TraderRange = "TraderRange"
                },
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 2,
                    State = State.Active,
                    TraderRange = "TraderRange"
                },
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 3,
                    State = State.Active,
                    TraderRange = "TraderRange"
                }
            };


            var expected = Mapper.Map<IEnumerable<CompanyInfo>>(list);

            var actual = list.Map<CompanyInfoDto>();

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void TestFor_MapToDtoOfList_ExpecteSuccess()
        {
            var list = new List<CompanyInfo>()
            {
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 1,
                    State = State.Active,
                    TraderRange = "TraderRange"
                },
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 2,
                    State = State.Active,
                    TraderRange = "TraderRange"
                },
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 3,
                    State = State.Active,
                    TraderRange = "TraderRange"
                }
            };

            var expected = Mapper.Map<List<CompanyInfo>>(list);

            var actual = list.Map<CompanyInfoDto>();

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void TestFor_MapToDtoOfQueryable_ExpecteSuccess()
        {
            var list = new List<CompanyInfo>
            {
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 1,
                    State = State.Active,
                    TraderRange = "TraderRange"
                },
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 2,
                    State = State.Active,
                    TraderRange = "TraderRange"
                },
                new CompanyInfo()
                {
                    CompanyName = "CompanyName",
                    Id = 3,
                    State = State.Active,
                    TraderRange = "TraderRange"
                }
            }.AsQueryable();

            var expected = Mapper.Map<IEnumerable<CompanyInfo>>(list);

            var actual = list.Map<CompanyInfoDto>();

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
