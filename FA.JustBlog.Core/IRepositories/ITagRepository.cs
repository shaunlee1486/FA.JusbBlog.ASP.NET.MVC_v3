﻿using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.IRepositories
{
    public interface ITagRepository:IGenericRepository<Tag>
    {
        //Tag Find(int TagId);
        //void AddTag(Tag Tag);
        //void UpdateTag(Tag Tag);
        //void DeleteTag(Tag Tag);
        //void DeleteTag(int TagId);
        //IList<Tag> GetAllTags();

        //1
        Tag FindTag(string name);
        void DeleteTag(Tag Tag);
        Tag GetTagByUrlSlug(string urlSlug);

    }
}
