namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Data.Entity.Core.Objects;
    using UserManagement.Models;

    public class user
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public short? Gender { get; set; }
        public string? Company { get; set; }
        public string? Title { get; set; }
        public string Email { get; set; } = null!;
        public string? Avatar { get; set; }
    }

    [Route("api/users")]
    [ApiController]
    public class WebUsersController : ControllerBase
    {
        

        private readonly UserManagementContext _context;

        public WebUsersController(UserManagementContext context)
        {
            this._context = context;
        }

        // GET: api/WebUsers
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult> GetWebUsers()
        {
          if (this._context.WebUsers == null)
          {
              return NotFound();
          }

          var listUser = await this._context.WebUsers
                        .Join(
                            this._context.Titles,
                            usr => usr.Title,
                            ttl => ttl.Id,
                            (usr, ttl) => new
                            {
                                newUser = new
                                {
                                    id = usr.Id,
                                    firstName = usr.FirstName,
                                    lastName = usr.LastName,
                                    dob = usr.DateOfBirth,
                                    gender = usr.Gender,
                                    company = usr.Company,
                                    title = ttl.Name,
                                    email = usr.Email,
                                    status = "Active",
                                },
                            }
                        ).ToListAsync();

          object [] users = Array.Empty<object>();
          var listusrs = users.ToList();
          foreach (var usr in listUser)
            {
                listusrs.Add(usr.newUser);
            };

          return Ok(listusrs);
        }

        // GET: api/WebUsers/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetWebUser(int id)
        {
          if (this._context.WebUsers == null)
          {
              return NotFound();
          }

            //var webUser = await this._context.WebUsers.FindAsync(id);
          var webUser = await this._context.WebUsers.Where(usr => usr.Id == id)
                          .Join(
                              this._context.Titles,
                              usr => usr.Title,
                              ttl => ttl.Id,
                              (usr, ttl) => new
                              {
                                  newUser = new
                                  {
                                      id = usr.Id,
                                      firstName = usr.FirstName,
                                      lastName = usr.LastName,
                                      dob = usr.DateOfBirth,
                                      gender = usr.Gender,
                                      company = usr.Company,
                                      title = ttl.Name,
                                      email = usr.Email,
                                      status = "Active",
                                  },
                              }
                          ).FirstAsync();
           
          if (webUser == null)
          {
              return NotFound();
          }

          return Ok(webUser.newUser);
        }

        // GET: api/WebUsers/nhuvdk@gmail.com
        [EnableCors("AllowOrigin")]
        [HttpGet("email/{email}")]
        public async Task<ActionResult> GetWebUser(string email)
        {
            if (this._context.WebUsers == null)
            {
                return NotFound();
            }

            //var webUser = await this._context.WebUsers.FindAsync(id);
            var webUser = await this._context.WebUsers.Where(usr => usr.Email == email)
                            .Join(
                                this._context.Titles,
                                usr => usr.Title,
                                ttl => ttl.Id,
                                (usr, ttl) => new
                                {
                                    newUser = new
                                    {
                                        id = usr.Id,
                                        firstName = usr.FirstName,
                                        lastName = usr.LastName,
                                        dob = usr.DateOfBirth,
                                        gender = usr.Gender,
                                        company = usr.Company,
                                        title = ttl.Name,
                                        email = usr.Email,
                                        status = "Active",
                                    },
                                }
                            ).FirstAsync();

            if (webUser == null)
            {
                return NotFound();
            }

            return Ok(webUser.newUser);
        }

        // PUT: api/WebUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebUser(int id, WebUser webUser)
        {
            if (id != webUser.Id)
            {
                return BadRequest();
            }

            this._context.Entry(webUser).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WebUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public async Task<ActionResult<WebUser>> PostWebUser(WebUser webUser)
        {
          this._context.WebUsers.Add(webUser);
          await this._context.SaveChangesAsync();

          return this.CreatedAtAction("GetWebUser", new { id = webUser.Id }, webUser);
        }

        // DELETE: api/WebUsers/5
        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebUser(int id)
        {
            if (this._context.WebUsers == null)
            {
                return NotFound();
            }
            var webUser = await this._context.WebUsers.FindAsync(id);
            if (webUser == null)
            {
                return NotFound();
            }

            this._context.WebUsers.Remove(webUser);
            await this._context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/WebUsers
        // SEARCH USER BY SEARCHTEXT
        [EnableCors("AllowOrigin")]
        [HttpPost("{searchText}")]
        public async Task<ActionResult<WebUser>> SearchUser(string searchText)
        {
            string userContent;
            object[] targetUsers = Array.Empty<object>();
            var listusrs = targetUsers.ToList();

            if (searchText == null)
            {
                return Problem("Search text is null.");
            }

            foreach (var webUser in this._context.WebUsers)
            {
                userContent = webUser.FirstName.ToLower() + ' ' + webUser.LastName.ToLower() + ' ' + webUser.Email.ToLower();

                if (userContent.Contains(searchText.ToLower()))
                {
                    var result = new user();

                    result.Id = webUser.Id;
                    result.FirstName = webUser.FirstName;
                    result.LastName = webUser.LastName;
                    result.Gender = webUser.Gender;
                    result.Avatar = webUser.Avatar;
                    result.DateOfBirth = webUser.DateOfBirth;
                    result.Email = webUser.Email;
                    result.Company = webUser.Company;

                    switch(webUser.Title)
                    {
                        case 1:
                            result.Title = "Team Lead";
                            break;
                        case 2:
                            result.Title = "Architecture";
                            break;
                        case 3:
                            result.Title = "Web Developer";
                            break;
                        case 4:
                            result.Title = "Tester";
                            break;
                        case 5:
                            result.Title = "UI/UX";
                            break;
                        case 6:
                            result.Title = "DBA";
                            break;
                    }

                    listusrs.Add(result);
                }
            }

            return Ok(listusrs);
        }

        private bool WebUserExists(int id)
        {
            return (this._context.WebUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
