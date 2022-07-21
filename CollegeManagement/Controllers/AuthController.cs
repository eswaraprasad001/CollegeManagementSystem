﻿using CollegeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace CollegeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class AuthController : ControllerBase
        {
        private readonly IConfiguration _configuration;
        private readonly collegemanagementContext _context;

            public AuthController(IConfiguration configuration,collegemanagementContext context)
            {
                _context = context;
                _configuration = configuration;
        }

            [HttpPost]
            [Route("register")]
            public async Task<ActionResult<User>> Register([FromBody] User user)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok("User Created Successfully");
            }

            [HttpPost]
            [Route("login")]
            public async Task<ActionResult<User>> Login([FromBody] Login user)
            {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email &&  x.Password == user.Password);
                if (dbUser == null)
                {
                    return BadRequest("User Not Found");
                }
                string token = CreateToken(user);
                return Ok(token);
        }

        private string CreateToken(Login user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,"1")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        [HttpGet]
        [Route("getusers")]
        public async Task<ActionResult<User>> GetUsers()
        {
            //var isAdmin = "";
            var re = Request;
            var headers = re.Headers;
            if (headers.ContainsKey("Authorization"))
            {
                var token = headers["Authorization"];
                if (token != 0)
                {
                    return Ok("allDetails");
                }
                
                return Ok("hel");
            }
            return Ok();

        }
        //[HttpGet]
        //[Route("cookies")]
        //public int Get()
        //{
        //    string key = "MyCookie";
        //    string value = "Asd1";
        //    CookieOptions cookieOptions = new CookieOptions();
        //    cookieOptions.Expires = DateTime.Now.AddDays(7);
        //    Response.Cookies.Append(key, value, cookieOptions);
        //    return 1;
        //}
        //[HttpGet]
        //[Route("read")]
        //public String read()
        //{
        //    string key = "MyCookie";
        //    String cookieValue=Request.Cookies[key];

        //    return cookieValue;

        //}
    }
    }

