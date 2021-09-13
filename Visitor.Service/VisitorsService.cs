using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Visitor.Core;
using Visitor.Domain.Model;
using Visitor.Domain.DTO;

namespace Visitor.Service
{
    public class VisitorsService : IVisitorsService
    {
        private IUnitOfWork _uow;
        public VisitorsService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IEnumerable<Visitor.Domain.DTO.VisitorDTO> GetVisitors()
        {
            try
            {
                IEnumerable<Visitor.Domain.DTO.VisitorDTO> visitors = _uow.GetRepository<Visitor.Domain.Model.Visitor>().GetAll()
                    .OrderBy(p => p.Name).Select(dto=>new Domain.DTO.VisitorDTO 
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    CheckInTime = dto.CheckInTime,
                    CheckOutTime = dto.CheckOutTime,
                    CheckInStatus = dto.CheckInStatus
                });

                return visitors;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Visitor.Domain.DTO.VisitorDTO GetCheckedInVisitor()
        {
            try
            {
                VisitorDTO visitor = _uow.GetRepository<Visitor.Domain.Model.Visitor>().Get(q => q.CheckInStatus == false, q=>q.CheckInTime, SortOrder.Ascending).Select(dto => new Domain.DTO.VisitorDTO
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    CheckInTime = dto.CheckInTime,
                    CheckOutTime = dto.CheckOutTime,
                    CheckInStatus = dto.CheckInStatus
                }).FirstOrDefault();

                return visitor;
                    
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public bool CreateVisitor(VisitorDTO dto)
        {
            IGenericRepository<Visitor.Domain.Model.Visitor> repository = this._uow.GetRepository<Visitor.Domain.Model.Visitor>();

            Visitor.Domain.Model.Visitor visitor = new Visitor.Domain.Model.Visitor
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                CheckInTime = dto.CheckInTime.ToLocalTime(),
                CheckOutTime = dto.CheckOutTime.ToLocalTime(),
                CheckInStatus = false
            };
            repository.Add(visitor);

            if (_uow.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateVisitor(VisitorDTO dto)
        {
            IGenericRepository<Visitor.Domain.Model.Visitor> repository = this._uow.GetRepository<Visitor.Domain.Model.Visitor>();
            Visitor.Domain.Model.Visitor visitor = repository.GetById(dto.Id);
            visitor.CheckInStatus = true;
            //visitor = new Visitor.Domain.Model.Visitor
            //{
            //    Id = dto.Id,
            //    Name = dto.Name,
            //    Address = dto.Address,
            //    Phone = dto.Phone,
            //    CheckInTime = dto.CheckInTime,
            //    CheckOutTime = dto.CheckOutTime,
            //    CheckInStatus = dto.CheckInStatus
            //};

            repository.Update(visitor);

            if (_uow.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
