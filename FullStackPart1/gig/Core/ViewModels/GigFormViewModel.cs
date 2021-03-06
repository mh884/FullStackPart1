﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigFormViewModel
    {

        public int id { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [FutureTime]
        public string Time { get; set; }
        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public string Heading { get; set; }

        public string Action
        {


            get
            {

                Expression<Func<GigsController, ActionResult>> update =
                      (c => c.Update(this));
                Expression<Func<GigsController, ActionResult>> Create =
                      (c => c.Create(this));


                var action = (id != 0) ? update : Create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");

        }
    }
}