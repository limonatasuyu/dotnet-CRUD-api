using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();

            var newCharacters = await _context.Characters.ToListAsync();
            response.Data = _mapper.Map<List<GetCharacterDto>>(newCharacters);
            response.Message = "Character successfully added!";
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            response.Data = _mapper.Map<List<GetCharacterDto>>(dbCharacters);
            response.Message = "Characters successfully received!";
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharactersById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            var character = _context.Characters.FirstOrDefault(c => c.Id == id);
            if (character is not null) {
                response.Data = _mapper.Map<GetCharacterDto>(character);
                response.Message = "Characters successfully received!";
            } else {
                response.Success = false;
                response.Message = "Character not found";
            }
            
            return response;


        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto theCharacter) {
            
            var response = new ServiceResponse<GetCharacterDto>();
            var character = _context.Characters.FirstOrDefault(c => c.Id == theCharacter.Id);
            if (character is not null) {
                character.Name = theCharacter.Name;
                character.HitPoints = theCharacter.HitPoints;
                character.Strength = theCharacter.Strength;
                character.Defense = theCharacter.Defense;
                character.Intelligence = theCharacter.Intelligence;
                character.Class = theCharacter.Class;

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
                response.Message = "Character Updated Successfully";
            } else {
                response.Success = false;
                response.Message = $"Character with Id '{theCharacter.Id}' not found";
            }

            return response;
        }

            public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id) {
            
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var character = _context.Characters.FirstOrDefault(c => c.Id == id);
            if (character is not null) {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                response.Message = "Character Deleted Successfully";
            } else {
                response.Success = false;
                response.Message = $"Character with Id '{id}' not found";
            }

            return response;

        }

    }
}