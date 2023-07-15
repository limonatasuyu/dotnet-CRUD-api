using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController] // i dont need to know attributes for now
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get() {
            return Ok(await _characterService.GetAllCharacters());
        }

       [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetSingle(int id) {

            return Ok(await _characterService.GetCharactersById(id));
        }

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter) {

            await _characterService.AddCharacter(newCharacter);
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpPut()]
        public async Task<ActionResult<ServiceResponse<UpdateCharacterDto>>> UpdateCharacter(UpdateCharacterDto theCharacter) {
            var response = await _characterService.UpdateCharacter(theCharacter);
            if (response.Data is null)
                return NotFound(response);  

            return Ok(response);
        }
        

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<UpdateCharacterDto>>> DeleteCharacter(int id) {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is null)
                return NotFound(response);

            return Ok(response);
        }
    }
}