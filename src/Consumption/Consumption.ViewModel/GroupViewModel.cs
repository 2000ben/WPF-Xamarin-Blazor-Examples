﻿/*
*
* 文件名    ：GroupViewModel                             
* 程序说明  : 用户组
* 更新时间  : 2020-08-04 17：32
* 联系作者  : QQ:779149549 
* 开发者群  : QQ群:874752819
* 邮件联系  : zhouhaogg789@outlook.com
* 视频教程  : https://space.bilibili.com/32497462
* 博客地址  : https://www.cnblogs.com/zh7791/
* 项目地址  : https://github.com/HenJigg/WPF-Xamarin-Blazor-Examples
* 项目说明  : 以上所有代码均属开源免费使用,禁止个人行为出售本项目源代码
*/


namespace Consumption.ViewModel
{
    using Consumption.Common.Contract;
    using Consumption.Core.Entity;
    using Consumption.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;
    using Core.Query;
    using Consumption.Core.Common;
    using System.Linq;

    /// <summary>
    /// 部门管理
    /// </summary>
    public class GroupViewModel : BaseDataViewModel<Group>
    {
        private readonly IConsumptionService service;
        public GroupViewModel()
        {
            SelectPageTitle = "部门管理";
            NetCoreProvider.Get(out service);
        }

        public override async Task GetPageData(int pageIndex)
        {
            try
            {
                var r = await service.GetGroupListAsync(new QueryParameters()
                {
                    PageIndex = PageIndex,
                    PageSize = PageSize,
                    Search = SearchText
                });
                if (r != null && r.success)
                {
                    GridModelList = new ObservableCollection<Group>();
                    this.TotalCount = r.dynamicObj.TotalCount;
                    r.dynamicObj.Items?.ToList().ForEach(arg =>
                    {
                        GridModelList.Add(arg);
                    });
                    base.SetPageCount();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
