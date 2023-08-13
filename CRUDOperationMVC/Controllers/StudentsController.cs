using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CRUDOperationMVC.Models;
using System.Net;

namespace CRUDOperationMVC.Controllers
{
    public class StudentsController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        // GET: Students
        public ActionResult Index()
        {
            List<Student> objlist = new List<Student>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpSelStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Student std = new Student();

                std.ID = Convert.ToInt32(dr["ID"]);
                std.Name = dr["Name"].ToString();
                std.Email = dr["Email"].ToString();
                std.Contact = dr["Contact"].ToString();
                objlist.Add(std);

            }

            con.Close();
            return View(objlist);
        }
        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student std)
        {

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpInsStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", std.Name);
            cmd.Parameters.AddWithValue("@Email", std.Email);
            cmd.Parameters.AddWithValue("@Contact", std.Contact);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if(i>0)
            {
                ViewData["message"] = "Data inserted successfully.";
               
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["message"] = "Not inserted data.";
            }

            con.Close();

            return View();
        }
        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
           
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpSelByIdStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Student std = new Student();
            while (dr.Read())
            {
                

                std.ID = Convert.ToInt32(dr["ID"]);
                std.Name = dr["Name"].ToString();
                std.Email = dr["Email"].ToString();
                std.Contact = dr["Contact"].ToString();
            }

            con.Close();
            return View(std);
        }
        // POST: Student/Edit
        [HttpPost]
        public ActionResult Edit(Student std)
        {

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpUpdateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", std.ID);
            cmd.Parameters.AddWithValue("@Name", std.Name);
            cmd.Parameters.AddWithValue("@Email", std.Email);
            cmd.Parameters.AddWithValue("@Contact", std.Contact);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >0)
            {
                ViewData["message"] = "Data Updated successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["message"] = "Not updated data.";
            }

            con.Close();

            return View();
        }
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpSelByIdStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Student std = new Student();
            while (dr.Read())
            {


                std.ID = Convert.ToInt32(dr["ID"]);
                std.Name = dr["Name"].ToString();
                std.Email = dr["Email"].ToString();
                std.Contact = dr["Contact"].ToString();
            }

            con.Close();
            return View(std);
        }
        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpSelByIdStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
          
            Student std = new Student();
            while (dr.Read())
            {


                std.ID = Convert.ToInt32(dr["ID"]);
                std.Name = dr["Name"].ToString();
                std.Email = dr["Email"].ToString();
                std.Contact = dr["Contact"].ToString();
            }

            con.Close();
            return View(std);
        }
        [HttpPost]
        public ActionResult Delete(Student std)
        {

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SpDeleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", std.ID);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >0)
            {
                ViewData["message"]= "Recordn Deleted successfully.";
                return RedirectToAction("Index");
               
            }
            else
            {
                ViewData["message"] = "Not Deleted data.";
            }
            return View();
        }
    }
}