﻿using EliteRecruit.Models;
using EliteRecruit.ViewModels;
using static EliteRecruit.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EliteRecruit.Interfaces
{
    public interface IStudentRepository : IDisposable
    {
        Task<IList<Student>> GetStudents(string filterBy, SortByParameter sortBy, StudentViewModel studentViewModel);
        Task<Student> GetStudentByID(int studentId);
        Task<Student> InsertStudent(StudentViewModel studentViewModel);
        Task<Student> UpdateStudent(StudentViewModel studentViewModel);
        Task UpdateStudentsImagePath();
        Task DeleteStudent(int ID);
        Task<IList<Student>> GetTop5StudentsByGPA();
        Task<Comment> GetCommentByID(int commentId);
        Task<Comment> EditComment(CommentViewModel commentViewModel);
        Task<Comment> InsertComment(CommentViewModel commentViewModel);
        Task DeleteCommentByID(int commentId);

        Task<PipelineStatus> GetPipelineStatusByStudentId(int studentId);
        Task<PipelineStatus> UpdatePipelineStatus(int studentId, StudentViewModel studentViewModel);
    }
}
