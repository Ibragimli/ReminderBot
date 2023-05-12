using Microsoft.EntityFrameworkCore;
using ReminderBot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderBot.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Reminder> Reminders { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
