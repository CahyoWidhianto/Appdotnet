using Latihan.Data;
using Latihan.Data.Enum;
using Latihan.Interfaces;
using Latihan.Models;
using Latihan.Repository;
using Latihan.Services;
using Latihan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Controllers
{
    public class RaceController : Controller
    {

        private readonly IPhotoService _photoService;
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository raceRepository, IPhotoService photoService) 
        { 
            _raceRepository = raceRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Filed");
            }
            return View(raceVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var raceVm = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = (int)race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory,
            };
            return View(raceVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race");
                return View("Edit", raceVM);
            }
            var userRace = await _raceRepository.GetByIdAsyncNoTracking(id);
            if (userRace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Clound not delete photo");
                    return View(raceVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,
                };

                _raceRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }
        }

    }
}
