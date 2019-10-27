﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace PengUserControls
    {
        /// <summary>
        /// 用户界面语言切换类
        /// </summary>
        public class CChangeLanguageForUI
        {
            /// <summary>
            /// 用户界面语言切换类的构造函数
            /// </summary>
            public CChangeLanguageForUI()
            {
            }

            #region "变量定义"

            /// <summary>
            /// 错误信息
            /// </summary>
            public static Queue<string> ErrorMessage = new Queue<string>();

            /// <summary>
            /// 软件作者
            /// </summary>
            public string Author
            {
                get { return "软件作者：彭东南  southeastofstar@163.com"; }
            }

            #endregion

            #region "函数代码"

            /// <summary>
            /// 【第一步、调用函数 SetUIInChinese 或者 SetUIInUKEnglish】；
            /// 【第二步、调用函数 ChangeLanguageOfUI】；
            /// ------------------------------------------------------------------------
            /// 修改UI界面语言，请先执行设置语言的函数，然后再执行此函数来调用对应的资源来更新UI：
            /// 特别注意：调用此函数后，窗体会恢复到初始状态，还需要根据情况再次设置界面的具体内容，例如 visible/enable等属性；
            /// </summary>
            /// <param name="TargetSource"目标窗体对应的组件资源对象，
            /// 请使用此格式来实例化对象：ComponentResourceManager resource = new ComponentResourceManager(typeof(目标窗体类名称));
            /// 例如：ComponentResourceManager resource = new ComponentResourceManager(typeof(Form1));></param>
            /// <param name="TargetControl">目标控件，可以是窗体(this)或者其它 Control 类型，这里只需要 this 就会自动将窗体里面所有的 Control 更新为对应语言的资源</param>
            public void ChangeLanguageOfUI(ComponentResourceManager TargetSource, Control TargetControl)
            {
                try
                {
                    TargetSource.ApplyResources(TargetControl, TargetControl.Name);
                    TargetControl.ResumeLayout();
                    //TargetControl.Update();
                    TargetControl.Refresh();

                    if (TargetControl is MenuStrip)
                    {
                        //菜单
                        MenuStrip tempMenu = TargetControl as MenuStrip;
                        tempMenu.SuspendLayout();
                        foreach (ToolStripMenuItem item in tempMenu.Items)
                        {
                            if (null != item)
                            {
                                TargetSource.ApplyResources(item, item.Name);
                                if (null != item.DropDownItems && item.DropDownItems.Count > 0)
                                {
                                    foreach (ToolStripMenuItem subitem in item.DropDownItems)
                                    {
                                        if (null != subitem)
                                        {
                                            TargetSource.ApplyResources(subitem, subitem.Name);
                                        }
                                    } 
                                }
                            }
                        }
                        tempMenu.ResumeLayout();
                        tempMenu.Update();
                        tempMenu.Refresh();
                    }
                    else if (TargetControl is StatusStrip)
                    {
                        //状态栏
                        StatusStrip tempStatusStrip = TargetControl as StatusStrip;
                        tempStatusStrip.SuspendLayout();
                        foreach (ToolStripStatusLabel item in tempStatusStrip.Items)
                        {
                            if (null != item)
                            {
                                TargetSource.ApplyResources(item, item.Name);
                            }
                        }
                        tempStatusStrip.ResumeLayout();
                        tempStatusStrip.Update();
                        tempStatusStrip.Refresh();
                    }
                    else if (TargetControl is ToolStrip)//如果传入的控件是StatusStrip，这里的判断条件也是 true，这可能是因为继承的关系
                    {
                        //工具栏按钮
                        ToolStrip tempToolStrip = TargetControl as ToolStrip;
                        tempToolStrip.SuspendLayout();
                        foreach (ToolStripButton item in tempToolStrip.Items)
                        {
                            if (null != item)
                            {
                                TargetSource.ApplyResources(item, item.Name);
                            }
                        }
                        tempToolStrip.ResumeLayout();
                        tempToolStrip.Update();
                        tempToolStrip.Refresh();
                    }
                    else if (TargetControl is Form)
                    {
                        //窗体
                        TargetSource.ApplyResources(TargetControl, "$this");
                        foreach (Control item in TargetControl.Controls)
                        {
                            if (null != item)
                            {
                                ChangeLanguageOfUI(TargetSource, item);
                            }
                        }
                        TargetControl.ResumeLayout();
                        TargetControl.Update();
                        TargetControl.Refresh();
                    }
                    else if (TargetControl is DataGridView)
                    {
                        //DataGridView
                        DataGridView tempDataGridView = TargetControl as DataGridView;
                        foreach (DataGridViewColumn item in tempDataGridView.Columns)
                        {
                            if (null != item)
                            {
                                TargetSource.ApplyResources(item, item.Name);
                            }
                        }
                        //tempDataGridView.Update();
                        tempDataGridView.Refresh();
                    }
                    else if (TargetControl is TreeView)
                    {
                        //TreeView
                        TreeView tempTreeView = TargetControl as TreeView;
                        if (tempTreeView.Nodes.Count > 0)
                        {
                            tempTreeView.SuspendLayout();
                            TreeNode[] tempTreeNodes = new TreeNode[tempTreeView.Nodes.Count];
                            for (int i = 0; i < tempTreeView.Nodes.Count; i++)
                            {
                                if (i == 0)
                                {
                                    tempTreeNodes[i] = (TreeNode)TargetSource.GetObject(tempTreeView.Name + ".Nodes");
                                }
                                else
                                {
                                    tempTreeNodes[i] = (TreeNode)TargetSource.GetObject(tempTreeView.Name + ".Nodes" + i.ToString());
                                }
                            }

                            tempTreeView.Nodes.Clear();
                            tempTreeView.Nodes.AddRange(tempTreeNodes);
                            tempTreeView.ResumeLayout();
                        }
                    }
                    else if (TargetControl is CheckedListBox)
                    {
                        //CheckedListBox
                        CheckedListBox tempCheckedListBox = TargetControl as CheckedListBox;
                        if (tempCheckedListBox.Items.Count > 0)
                        {
                            object[] tempCheckedListBoxItems = new object[tempCheckedListBox.Items.Count];
                            for (int i = 0; i < tempCheckedListBox.Items.Count; i++)
                            {
                                if (i == 0)
                                {
                                    tempCheckedListBoxItems[i] = TargetSource.GetString(tempCheckedListBox.Name + ".Items");
                                }
                                else
                                {
                                    tempCheckedListBoxItems[i] = TargetSource.GetString(tempCheckedListBox.Name + ".Items" + i.ToString());
                                }
                            }

                            tempCheckedListBox.Items.Clear();
                            tempCheckedListBox.Items.AddRange(tempCheckedListBoxItems);

                            tempCheckedListBox.ResumeLayout();
                            tempCheckedListBox.Update();
                            tempCheckedListBox.Refresh();
                        }
                    }
                    else if (TargetControl is ListBox)
                    {
                        //ListBox
                        ListBox tempListBox = TargetControl as ListBox;
                        if (tempListBox.Items.Count > 0)
                        {
                            object[] tempListBoxItems = null;// new object[tempListBox.Items.Count];
                            int iItemIndexCount = 0;
                            string TempItem = "";

                            //发生错误：值不能为 null。
                            //参数名: item;    在 System.Windows.Forms.ListBox.ObjectCollection.AddInternal(Object item)

                            for (int i = 0; i < tempListBox.Items.Count; i++)
                            {
                                if (i == 0)
                                {
                                    TempItem = TargetSource.GetString(tempListBox.Name + ".Items");
                                    if (null != TempItem && TempItem != "")
                                    {
                                        tempListBoxItems = new object[1];
                                        //Array.Resize<object>(ref tempListBoxItems, iItemIndexCount + 1);
                                        tempListBoxItems[iItemIndexCount] = TempItem;
                                        iItemIndexCount++;
                                    }
                                }
                                else
                                {
                                    TempItem = TargetSource.GetString(tempListBox.Name + ".Items" + i.ToString());
                                    if (null != TempItem && TempItem != "")
                                    {
                                        Array.Resize<object>(ref tempListBoxItems, iItemIndexCount + 1);
                                        tempListBoxItems[iItemIndexCount] = TempItem;
                                        iItemIndexCount++;
                                    }
                                }
                            }

                            if (null != tempListBoxItems)
                            {
                                tempListBox.Items.Clear();
                                tempListBox.Items.AddRange(tempListBoxItems);

                                tempListBox.ResumeLayout();
                                tempListBox.Update();
                                tempListBox.Refresh();
                            }
                            else
                            {
                                ErrorMessage.Enqueue("窗体 " + TargetControl.FindForm().Name + " 的ListBox控件 " + TargetControl.Name + " 子项为空或者没有建立多语言版本的资源");
                            }
                        }
                    }
                    else if (TargetControl is ListView)
                    {
                        //ListView
                        ListView tempListView = TargetControl as ListView;
                        if (tempListView.Items.Count > 0)
                        {
                            ListViewItem[] tempTreeNodes = new ListViewItem[tempListView.Items.Count];
                            for (int i = 0; i < tempListView.Items.Count; i++)
                            {
                                if (i == 0)
                                {
                                    tempTreeNodes[i] = (ListViewItem)TargetSource.GetObject(tempListView.Name + ".Items");
                                }
                                else
                                {
                                    tempTreeNodes[i] = (ListViewItem)TargetSource.GetObject(tempListView.Name + ".Items" + i.ToString());
                                }
                            }

                            tempListView.Items.Clear();
                            tempListView.Items.AddRange(tempTreeNodes);
                        }

                        if (tempListView.Columns.Count > 0)
                        {
                            for (int i = 0; i < tempListView.Columns.Count; i++)
                            {
                                TargetSource.ApplyResources(tempListView.Columns[i], "columnHeader" + (i + 1).ToString());
                            }
                        }

                        tempListView.ResumeLayout();
                        tempListView.Update();
                        tempListView.Refresh();
                    }
                    else if (TargetControl is ComboBox)
                    {
                        //ComboBox
                        ComboBox tempComboBox = TargetControl as ComboBox;
                        
                        if (tempComboBox.Items.Count > 0)
                        {
                            tempComboBox.SuspendLayout();
                            object[] AllItems = null;// new object[1];//tempComboBox.Items.Count
                            int iItemIndexCount = 0;
                            string TempItem = "";

                            // 发生错误：值不能为 null。
                            // 参数名: item;    在 System.Windows.Forms.ComboBox.ObjectCollection.AddInternal(Object item)
                            
                            for (int i = 0; i < tempComboBox.Items.Count; i++)
                            {
                                if (iItemIndexCount == 0)
                                {
                                    //Array.Resize<object>(ref AllItems, iItemIndexCount + 1);
                                    TempItem = TargetSource.GetString(tempComboBox.Name + ".Items");
                                    if (null != TempItem && TempItem != "")
                                    {
                                        AllItems = new object[1];
                                        AllItems[iItemIndexCount] = TempItem;
                                        iItemIndexCount++;
                                    }
                                    //
                                }
                                else
                                {
                                    TempItem = TargetSource.GetString(tempComboBox.Name + ".Items" + i.ToString());
                                    if (null != TempItem && TempItem != "")
                                    {
                                        Array.Resize<object>(ref AllItems, iItemIndexCount + 1);
                                        AllItems[iItemIndexCount] = TempItem;
                                        iItemIndexCount++;
                                    }
                                }
                            }

                            if (null != AllItems)
                            {
                                tempComboBox.Items.Clear();
                                tempComboBox.Items.AddRange(AllItems);
                                tempComboBox.SelectedIndex = 0;

                                tempComboBox.ResumeLayout();
                                //tempComboBox.Update();
                                tempComboBox.Refresh();
                            }
                            else
                            {
                                ErrorMessage.Enqueue("窗体 " + TargetControl.FindForm().Name + " 的ComboBox控件 " + TargetControl.Name + " 子项为空或者没有建立多语言版本的资源");
                            }
                        }
                    }
                    else
                    {
                        if (TargetControl.HasChildren == true)
                        {
                            foreach (Control item in TargetControl.Controls)
                            {
                                if (null != item)
                                {
                                    ChangeLanguageOfUI(TargetSource, item);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //throw ex;
                    ErrorMessage.Enqueue(DateTime.Now.ToString() + "*-*" + "发生错误：" + ex.Message + "; " + ex.StackTrace);
                }
            }

            /// <summary>
            /// 获取当前 System.Globalization.CultureInfo 的区域性标识符，即所使用语言的国家名称代码
            /// </summary>
            /// <returns></returns>
            public int GetLCIDOfCurrentEnviroment()
            {
                int iRet = 0;
                try
                {
                    iRet = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
                }
                catch (Exception ex)
                {
                    //return iRet;
                    ErrorMessage.Enqueue(DateTime.Now.ToString() + "*-*" + "发生错误：" + ex.Message + "; " + ex.StackTrace);
                }
                return iRet;
            }

            /// <summary>
            /// 获取当前 System.Globalization.CultureInfo 的区域性名称，即所使用语言的国家名称简称
            /// </summary>
            /// <returns></returns>
            public string GetNameOfCurrentEnviroment()
            {
                string sRet = "";
                try
                {
                    sRet = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
                }
                catch (Exception ex)
                {
                    //return iRet;
                    ErrorMessage.Enqueue(DateTime.Now.ToString() + "*-*" + "发生错误：" + ex.Message + "; " + ex.StackTrace);
                }
                return sRet;
            }

            /// <summary>
            /// 设置使用 中文 资源来更新显示界面
            /// </summary>
            /// <returns></returns>
            public bool SetUIInChinese()
            {
                bool bRet = false;
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
                    bRet = true;
                }
                catch (Exception ex)
                {
                    bRet = false;
                    ErrorMessage.Enqueue(DateTime.Now.ToString() + "*-*" + "发生错误：" + ex.Message + "; " + ex.StackTrace);
                }
                return bRet;
            }

            /// <summary>
            /// 设置使用 英式英语 资源来更新显示界面
            /// </summary>
            /// <returns></returns>
            public bool SetUIInUKEnglish()
            {
                bool bRet = false;
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");
                    bRet = true;
                }
                catch (Exception ex)
                {
                    bRet = false;
                    ErrorMessage.Enqueue(DateTime.Now.ToString() + "*-*" + "发生错误：" + ex.Message + "; " + ex.StackTrace);
                }
                return bRet;
            }

            /// <summary>
            /// 获取当前 System.Globalization.CultureInfo 的区域性是否为中文
            /// </summary>
            /// <returns></returns>
            public bool IsInChinese()
            {
                bool bRet = false;
                try
                {
                    string sTempCultureName = GetNameOfCurrentEnviroment();
                    if (null != sTempCultureName && sTempCultureName == "zh-CN")
                    {
                        bRet = true;
                    }
                }
                catch (Exception ex)
                {
                    bRet = false;
                    ErrorMessage.Enqueue(DateTime.Now.ToString() + "*-*" + "发生错误：" + ex.Message + "; " + ex.StackTrace);
                }
                return bRet;
            }

            /// <summary>
            /// 获取当前 System.Globalization.CultureInfo 的区域性是否为英式英语
            /// </summary>
            /// <returns></returns>
            public bool IsInUKEnglish()
            {
                bool bRet = false;
                try
                {
                    string sTempCultureName = GetNameOfCurrentEnviroment();
                    if (null != sTempCultureName && sTempCultureName == "en-GB")
                    {
                        bRet = true;
                    }
                }
                catch (Exception ex)
                {
                    bRet = false;
                    ErrorMessage.Enqueue(DateTime.Now.ToString() + "*-*" + "发生错误：" + ex.Message + "; " + ex.StackTrace);
                }
                return bRet;
            }

            #endregion

        }//class

    }//namespace