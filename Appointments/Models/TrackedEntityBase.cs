﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Appointments.Api.Models {
    public abstract class TrackedEntityBase {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedUtc { get; private set; }

        //[Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime LastUpdateUtc { get { return DateTime.UtcNow; } private set { } }
    }
}