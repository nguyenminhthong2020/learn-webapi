using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoasController : ControllerBase
    {

        public static List<HangHoa> hangHoas = new List<HangHoa>();


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas); // 200
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var idGuild = Guid.Parse(id);
                var hangHoa = hangHoas.FirstOrDefault(x => x.MaHangHoa == idGuild);

                if (hangHoa == null)
                {
                    return NotFound(); // 404
                }
                else
                {
                    return Ok(hangHoa);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(HangHoaVM hanghoaVM)
        {
            var hanghoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hanghoaVM.TenHangHoa,
                DonGia = hanghoaVM.DonGia,
            };

            hangHoas.Add(hanghoa);

            return Ok(new  // khỏi cần new JsonResult cũng ra ok :V
            {
                Success = true,
                Data = hanghoa
            });
        }


        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hanghoaEdit)
        {
            try
            {
                var idGuild = Guid.Parse(id);
                var hangHoa = hangHoas.FirstOrDefault(x => x.MaHangHoa == idGuild);

                if (hangHoa == null)
                {
                    return NotFound(); // 404
                }
                else
                {
                    // id: truyền qua url
                    // hanghoa : truyền qua body
                    if (id != hangHoa.MaHangHoa.ToString())
                    {
                        return BadRequest();
                    }
                    //Update
                    hangHoa.TenHangHoa = hanghoaEdit.TenHangHoa;
                    hangHoa.DonGia = hanghoaEdit.DonGia;

                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var idGuild = Guid.Parse(id);
                var hangHoa = hangHoas.FirstOrDefault(x => x.MaHangHoa == idGuild);

                if (hangHoa == null)
                {
                    return NotFound(); // 404
                }
                else
                {
                    //Delete
                    hangHoas.Remove(hangHoa);

                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
