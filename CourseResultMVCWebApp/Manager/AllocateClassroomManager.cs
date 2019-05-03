using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseResultMVCWebApp.Gateway;
using CourseResultMVCWebApp.Models;

namespace CourseResultMVCWebApp.Manager
{
    public class AllocateClassroomManager : Manager
    {
        public List<Course> GetCourseByDepartment(int departmentId)
        {
            return AllocateClassroomGateway.GetCourseByDepartment(departmentId);
        }
        public List<Room> GetAllRoom()
        {
            List<Room> rooms = AllocateClassroomGateway.GetAllRoom();
            return rooms;
        }

        public string Save(AllocateClassroom allocate)
        {
            string fromH = allocate.FromTime.ToString();
            string fromHSub = fromH.Substring(0, 2);
            string fromM = allocate.FromTime.ToString();
            string fromMSub = fromM.Substring(3, 2);
            int fromMin = Convert.ToInt32(fromHSub) * 60 + Convert.ToInt32(fromMSub);
            string toH = allocate.ToTime.ToString();
            string toHSub = toH.Substring(0, 2);
            string toM = allocate.ToTime.ToString();
            string toMSub = toM.Substring(3, 2);
            int toMin = Convert.ToInt32(toHSub) * 60 + Convert.ToInt32(toMSub); 
            allocate.FromTime = fromMin.ToString();
            allocate.ToTime = toMin.ToString();
            List<AllocateClassroom> classrooms = AllocateClassroomGateway.IsScheduleExists(allocate);
            if (classrooms.Count > 0)
            {
                foreach (AllocateClassroom allocateClassroom in classrooms)
                {

                    int from = Convert.ToInt32(allocateClassroom.FromTime);
                    int to = Convert.ToInt32(allocateClassroom.ToTime);

                    if (fromMin >= from && fromMin < to)
                    {
                        return "Schedule allready allocated";
                    }
                    else if (toMin > from && toMin <= to)
                    {
                        return "Schedule allready allocated";
                    }
                    else if (fromMin <= from && toMin >= to)
                    {

                        return "Schedule allready allocated";
                    }
                    else
                    {
                        //Ultimately save
                        int isRowAffected = AllocateClassroomGateway.Save(allocateClassroom);
                        return "Room Allocated " + Message(isRowAffected);
                    }

                }
                return "Room Allocated successfully";
            }
            else
            {
                int isRowAffected = AllocateClassroomGateway.Save(allocate);
                return "Room Allocated " + Message(isRowAffected);
            }
        }
    }
}