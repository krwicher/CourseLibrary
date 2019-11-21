using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers.CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/author-collections")]
    public class AuthorCollectionController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }

        [HttpGet("{{ids}}", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection(
            [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
                return BadRequest();

            var authorIds = ids as Guid[] ?? ids.ToArray();
            var authorEntities = _courseLibraryRepository.GetAuthors(authorIds);
            if (authorIds.Count() != authorEntities.Count())
                return NotFound();
            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

            return Ok(authorsToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(
            IEnumerable<AuthorForCreationDto> authorCollection)
        {
            var authorEntities = _mapper.Map<IEnumerable<Author>>(authorCollection);
            foreach (var author in authorEntities)
            {
                _courseLibraryRepository.AddAuthor(author);
            }

            _courseLibraryRepository.Save();

            var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var idsAsString = string.Join(",", authorCollectionToReturn.Select(x => x.Id));

            return CreatedAtRoute("GetAuthorCollection", new
            {
                ids = idsAsString
            }, authorCollectionToReturn);
        }
    }
}