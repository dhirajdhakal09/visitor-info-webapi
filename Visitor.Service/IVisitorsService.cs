using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Visitor.Domain.Model;

namespace Visitor.Service
{
    public interface IVisitorsService
    {
        IEnumerable<Visitor.Domain.DTO.VisitorDTO> GetVisitors();
        Visitor.Domain.DTO.VisitorDTO GetCheckedInVisitor();
        bool UpdateVisitor(Domain.DTO.VisitorDTO dto);
        bool CreateVisitor(Domain.DTO.VisitorDTO dto);
    }
}
