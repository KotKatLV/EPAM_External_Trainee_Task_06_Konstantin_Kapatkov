﻿using ResultsOfTheSession.DAO;
using ResultsOfTheSession.ORM.Models;
using ResultsOfTheSession.ORM.Models.Session;
using ResultsOfTheSession.PreparationOfReports.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultsOfTheSession.PreparationOfReports.Abstract
{
    public abstract class Report : IReport
    {
        protected Report(string connectionString)
        {
            DaoFactory = DaoFactory.GetInstance(connectionString);

            Sessions = Task.Run(async () => await DaoFactory.GetSession().ReadAllAsync().ConfigureAwait(false)).Result.ToList();
            SessionResults = Task.Run(async () => await DaoFactory.GetSessionResult().ReadAllAsync().ConfigureAwait(false)).Result.ToList();
            SessionSchedules = Task.Run(async () => await DaoFactory.GetSessionSchedule().ReadAllAsync().ConfigureAwait(false)).Result.ToList();
            Groups = Task.Run(async () => await DaoFactory.GetGroup().ReadAllAsync().ConfigureAwait(false)).Result.ToList();
            KnowledgeAssessmentForms = Task.Run(async () => await DaoFactory.GetKnowledgeAssessmentForm().ReadAllAsync().ConfigureAwait(false)).Result.ToList();
            Students = Task.Run(async () => await DaoFactory.GetStudent().ReadAllAsync().ConfigureAwait(false)).Result.ToList();
            Subjects = Task.Run(async () => await DaoFactory.GetSubject().ReadAllAsync().ConfigureAwait(false)).Result.ToList();
        }

        public DaoFactory DaoFactory { get; set; }

        public List<Session> Sessions { get; set; }

        public List<SessionResult> SessionResults { get; set; }

        public List<SessionSchedule> SessionSchedules { get; set; }

        public List<Group> Groups { get; set; }

        public List<KnowledgeAssessmentForm> KnowledgeAssessmentForms { get; set; }

        public List<Student> Students { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}