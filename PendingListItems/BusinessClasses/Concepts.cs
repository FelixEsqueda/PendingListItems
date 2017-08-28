using PendingListItems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;

namespace PendingListItems.BusinessClasses
{
    public class Concepts
    {
        public void UpdateAmountsSummary(ConceptModel concept)
        {
            using (DataContext.DataContext db = new DataContext.DataContext())
            {
                SummaryModel summary = db.Summary.Where(t => t.SummaryId == concept.SummaryId).FirstOrDefault();

                if (summary != null)
                {
                    decimal? UpdatedAmount = 0;
                    decimal? Prepayment = 0;
                    summary.TotalAmount = db.Concept.Where(t => t.SummaryId == summary.SummaryId).Sum(t => (decimal?)t.Amount);
                    UpdatedAmount = db.Concept.Where(t => t.SummaryId == summary.SummaryId && t.Payd).Sum(t => (decimal?)t.Amount) ?? (decimal)0;
                    Prepayment = db.Concept.Where(t => t.SummaryId == summary.SummaryId && !t.Payd && t.Prepayment > 0).Sum(t => (decimal?)t.Prepayment) ?? (decimal)0;

                    summary.UpdatedAmount = UpdatedAmount.Value + Prepayment;

                    db.SaveChanges();
                }
            }
        }
    }
}