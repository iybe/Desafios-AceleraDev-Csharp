using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Exceptions;
using Modelos;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private Dictionary<long, Time> times = new Dictionary<long, Time>();
        private Dictionary<long, Jogador> jogadores = new Dictionary<long, Jogador>();
        private Dictionary<long, long> relacaoTimeCapitao = new Dictionary<long, long>();

        public SoccerTeamsManager()
        {
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (times.ContainsKey(id)) 
            {
                throw new UniqueIdentifierException();
            }
            else 
            {
                times.Add(id, new Time(id, name, createDate, mainShirtColor, secondaryShirtColor));
            }
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (jogadores.ContainsKey(id)) 
            {
                throw new UniqueIdentifierException();
            }
            else if(!times.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else 
            {
                jogadores.Add(id, new Jogador(id, teamId, name, birthDate, skillLevel, salary));
            }
        }

        public void SetCaptain(long playerId)
        {
            if(!jogadores.ContainsKey(playerId))
            {
                throw new PlayerNotFoundException();
            }
            else
            {
                relacaoTimeCapitao[ jogadores[playerId].teamId ] = playerId;
            }
        }

        public long GetTeamCaptain(long teamId)
        {
            if(!times.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else if(!relacaoTimeCapitao.ContainsKey(teamId))
            {
                throw new CaptainNotFoundException();
            }
            else
            {
                return relacaoTimeCapitao[teamId];
            }
        }

        public string GetPlayerName(long playerId)
        {
            if (!jogadores.ContainsKey(playerId))
            {
                throw new PlayerNotFoundException();
            }
            else
            {
                return jogadores[playerId].name;
            }
        }

        public string GetTeamName(long teamId)
        {
            if (!times.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                return times[teamId].name;
            }
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            if (!times.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                return (from player in jogadores
                        where player.Value.teamId == teamId
                        orderby player.Value.id ascending
                        select player.Value.id
                    ).ToList();
            }
        }

        public long GetBestTeamPlayer(long teamId)
        {
            if (!times.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                return (from player in jogadores
                        where player.Value.teamId == teamId
                        orderby player.Value.skillLevel descending
                        select player.Value.id
                    ).ToList()[0];
            }
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            if (!times.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else 
            {
                return (from player in jogadores
                        where player.Value.teamId == teamId
                        orderby player.Value.birthDate ascending
                        select player.Value.id
                    ).ToList()[0];
            }
        }

        public List<long> GetTeams()
        {
            return (from time in times
                    orderby time.Value.id ascending
                    select time.Value.id
                ).ToList();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            if (!times.ContainsKey(teamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                return (from player in jogadores
                        where player.Value.teamId == teamId
                        orderby player.Value.salary descending, player.Value.id ascending
                        select player.Value.id
                ).ToList()[0];
            }
        }

        public decimal GetPlayerSalary(long playerId)
        {
            if (!jogadores.ContainsKey(playerId))
            {
                throw new PlayerNotFoundException();
            }
            else
            {
                return jogadores[playerId].salary;
            }
        }

        public List<long> GetTopPlayers(int top)
        {
            return (from player in jogadores
                    orderby player.Value.skillLevel descending, player.Value.id ascending
                    select player.Value.id
                ).ToList().GetRange(0,top);
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            if (!times.ContainsKey(teamId) || !times.ContainsKey(visitorTeamId))
            {
                throw new TeamNotFoundException();
            }
            else
            {
                if(times[teamId].corUniformePrincipal == times[visitorTeamId].corUniformePrincipal)
                {
                    return times[visitorTeamId].corUniformeSecundario;
                }
                else
                {
                    return times[visitorTeamId].corUniformePrincipal;
                }
            }
        }

    }
}
