using API.Authorization;
using API.DTOs;
using API.Services.Interfaces;
using API.Utilities;
using API.Utilities.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class GamesController(IGamesService gamesService) : Controller
{
    /// <summary>
    /// Amend game data
    /// </summary>
    /// <param name="id">The game id</param>
    /// <param name="patch">JsonPatch data</param>
    /// <response code="404">If the provided id does not belong to a game</response>
    /// <response code="400">If JsonPatch data is malformed</response>
    /// <response code="200">Returns the patched game</response>
    [Authorize(Roles = OtrClaims.Roles.Admin)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<GameDTO>(StatusCodes.Status200OK)]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] JsonPatchDocument<GameDTO> patch)
    {
        // Ensure target tournament exists
        GameDTO? game = await gamesService.GetAsync(id);
        if (game is null)
        {
            return NotFound();
        }

        // Ensure request is only attempting to perform a replace operation.
        if (!patch.IsReplaceOnly())
        {
            return BadRequest();
        }

        // Patch and validate
        patch.ApplyTo(game, ModelState);
        if (!TryValidateModel(game))
        {
            return BadRequest(ModelState.ErrorMessage());
        }

        // Apply patched values to entity
        GameDTO? updatedGame = await gamesService.UpdateAsync(id, game);
        return Ok(updatedGame!);
    }
}
