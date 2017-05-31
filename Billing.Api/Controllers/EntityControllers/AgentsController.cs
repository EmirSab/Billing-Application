using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/agents")]
    public class AgentsController : BaseController
    {
        [Route("")]
        [TokenAuthorization("admin,user")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(UnitOfWork.Agents.Get().ToList().Select(x => Factory.Create(x)).ToList());
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
            
        }

        [Route("{name}")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Agents.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("{id:int}")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Agent agent = UnitOfWork.Agents.Get(id);
                if (agent == null)
                {
                    return NotFound();
                }
                else
                {             
                    return Ok(Factory.Create(agent));
                }
            }
            catch(Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        // insert Agent
        [Route("")]
        [TokenAuthorization("admin")]
        public IHttpActionResult Post(AgentModel model)
        {
            try
            {
                Agent agent = Factory.Create(model);
                UnitOfWork.Agents.Insert(agent);
                UnitOfWork.Agents.Commit();
                return Ok(Factory.Create(agent));
            }
            catch(Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //update agent
        [Route("{id:int}")]
        [TokenAuthorization("admin,user")]
        public IHttpActionResult Put(int id, AgentModel model)
        {
            try
            {
                Agent agent = Factory.Create(model);
                UnitOfWork.Agents.Update(agent, id);
                UnitOfWork.Agents.Commit();
                return Ok(Factory.Create(agent));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //delete
        [Route("{id:int}")]
        [TokenAuthorization("admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Agent agent = UnitOfWork.Agents.Get(id);
                if (agent == null) return NotFound();
                UnitOfWork.Agents.Delete(id);
                UnitOfWork.Agents.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("profiles")]
        [HttpGet]
        public IHttpActionResult CreateProfiles()
        {
            string[] users = new string[1] { "user" };
            string[] admins = new string[2] { "admin", "user" };

            WebSecurity.InitializeDatabaseConnection("Billing", "Agents", "Id", "Username", autoCreateTables: true);
            Roles.CreateRole("admin");
            Roles.CreateRole("user");
            foreach (var agent in UnitOfWork.Agents.Get().ToList())
            {
                if (string.IsNullOrWhiteSpace(agent.Username))
                {
                    string[] names = agent.Name.Split(' ');
                    string username = names[0].ToLower();
                    agent.Username = username;
                    UnitOfWork.Agents.Update(agent, agent.Id);
                    UnitOfWork.Commit();
                }
                WebSecurity.CreateAccount(agent.Username, "billing", false);
                if (agent.Username == "marlon" || agent.Username == "julia") Roles.AddUserToRole(agent.Username, "admin"); else; Roles.AddUserToRole(agent.Username, "user");
            }
            if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection("Billing", "Agents", "Id", "Username", autoCreateTables: true);
            return Ok("user profiles created");
        }
    }
}
