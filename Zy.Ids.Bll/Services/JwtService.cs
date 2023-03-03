using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AutoMapper.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.Ids.Bll.Interfaces;
using Zy.Ids.Bll.Models;
using Zy.User.DAL.Entitys;

namespace Zy.Ids.Bll.Services
{
    public class JwtService : IJwtService
    {
        public UserManager<UserEntity> userManager;

        public Microsoft.Extensions.Configuration.IConfiguration configuration;

        public JwtService(UserManager<UserEntity> userManager, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<ServiceResult<GenerateTokenBo>> GenerateToken(GenerateTokenQueryBo queryBo)
        {

            var user = await this.userManager.FindByNameAsync(queryBo.Username);

            if (user == null)
            {
                return this.NotFound("用户名：", queryBo.Username).As<GenerateTokenBo>();
            }

            var checkPassword = await this.userManager.CheckPasswordAsync(user, queryBo.Password);

            if (!checkPassword)
            {
                return this.Error<GenerateTokenBo>($"用户名{queryBo.Username} 密码错误");
            }

            List<Claim> claims = new();
           
            claims.Add(new Claim(ClaimTypes.Name, queryBo.Username));
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("loginTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

            var ascll = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("qwsadas1213ndsjfsdkfdsf12"));
            var credential = new SigningCredentials(ascll, SecurityAlgorithms.HmacSha256);

            var val = DateTime.Now;

            var exp = val.AddHours(1);

            var exp2 = exp.AddHours(2);

            var security = GenerateJwtSecurityToken("ValidIssuer", "asdasd", claims, val, exp, credential);

            var security2 = GenerateJwtSecurityToken("ValidIssuer", "asdasd", claims, val, exp.AddHours(2), credential);

            var token = new JwtSecurityTokenHandler().WriteToken(security);

            var resToken = new JwtSecurityTokenHandler().WriteToken(security2);

            var result = new GenerateTokenBo
            {
                Access_token = token,
                Token_type = "password",
                Refresh_token = resToken,
                Expires_in = (exp2 - exp).TotalMilliseconds.ToString()
            };

            return this.Ok(result);
        }

        private JwtSecurityToken GenerateJwtSecurityToken(string issuer, string audience,
            List<Claim> claims, DateTime v, DateTime ex, SigningCredentials signing)
        => new(
                 issuer,
                 audience,
                 claims,
                 v,
                 ex,
                 signing);
    }
}
