﻿#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DG.Data.Model.Helpers;
using DG.UI.GHF;
using DG.DentneD.Model;
using DG.DentneD.Model.Entity;
using DG.DentneD.Forms.Objects;
using DentneD;
using Zuby.ADGV;

namespace DG.DentneD.Forms
{
    public partial class FormTreatmentsTypes : DGUIGHFForm
    {
        private DentneDModel _dentnedModel = null;

        private TabElement tabElement_tabTreatmentsTypes = new TabElement();

        /// <summary>
        /// Constructor
        /// </summary>
        public FormTreatmentsTypes()
        {
            InitializeComponent();

            Initialize(Program.uighfApplication);

            _dentnedModel = new DentneDModel();
        }

        /// <summary>
        /// Initialize TabElements
        /// </summary>
        protected override void InitializeTabElements()
        {
            //set Readonly OnSetEditingMode for Controls
            DisableReadonlyCheckOnSetEditingModeControlCollection.Add(typeof(DataGridView));
            DisableReadonlyCheckOnSetEditingModeControlCollection.Add(typeof(AdvancedDataGridView));

            //set Main BindingSource
            BindingSourceMain = vTreatmentsTypesBindingSource;
            GetDataSourceMain = GetDataSource_main;

            //set Main TabControl
            TabControlMain = tabControl_main;

            //set Main Panels
            PanelFiltersMain = null;
            PanelListMain = panel_list;
            PanelsExtraMain = null;

            //set tabTreatmentsTypes
            tabElement_tabTreatmentsTypes = new TabElement()
            {
                TabPageElement = tabPage_tabTreatmentsTypes,
                ElementItem = new TabElement.TabElementItem()
                {
                    PanelData = panel_tabTreatmentsTypes_data,
                    PanelActions = panel_tabTreatmentsTypes_actions,
                    PanelUpdates = panel_tabTreatmentsTypes_updates,

                    BindingSourceList = vTreatmentsTypesBindingSource,
                    GetDataSourceList = GetDataSource_main,

                    BindingSourceEdit = treatmentstypesBindingSource,
                    GetDataSourceEdit = GetDataSourceEdit_tabTreatmentsTypes,
                    AfterSaveAction = AfterSaveAction_tabTreatmentsTypes,

                    AddButton = button_tabTreatmentsTypes_new,
                    UpdateButton = button_tabTreatmentsTypes_edit,
                    RemoveButton = button_tabTreatmentsTypes_delete,
                    SaveButton = button_tabTreatmentsTypes_save,
                    CancelButton = button_tabTreatmentsTypes_cancel,

                    Add = Add_tabTreatmentsTypes,
                    Update = Update_tabTreatmentsTypes,
                    Remove = Remove_tabTreatmentsTypes
                }
            };

            //set Elements
            TabElements.Add(tabElement_tabTreatmentsTypes);
        }

        /// <summary>
        /// Loader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTreatmentsTypes_Load(object sender, EventArgs e)
        {
            ReloadView();

            advancedDataGridView_main.SortASC(advancedDataGridView_main.Columns[1]);
        }

        /// <summary>
        /// Get main list DataSource
        /// </summary>
        /// <returns></returns>
        private object GetDataSource_main()
        {
            IEnumerable<VTreatmentsTypes> vTreatmentsTypes =
                _dentnedModel.TreatmentsTypes.List().Select(
                r => new VTreatmentsTypes
                {
                    treatmentstypes_id = r.treatmentstypes_id,
                    name = r.treatmentstypes_name
                }).ToList();

            return DGDataTableUtils.ToDataTable<VTreatmentsTypes>(vTreatmentsTypes);
        }

        /// <summary>
        /// Main Datagrid filter handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advancedDataGridView_main_FilterStringChanged(object sender, EventArgs e)
        {
            vTreatmentsTypesBindingSource.Filter = advancedDataGridView_main.FilterString;
        }
        
        /// <summary>
        /// Main Datagrid sort handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advancedDataGridView_main_SortStringChanged(object sender, EventArgs e)
        {
            vTreatmentsTypesBindingSource.Sort = advancedDataGridView_main.SortString;
        }


        #region tabTreatmentsTypes

        /// <summary>
        /// Load the tab DataSource
        /// </summary>
        /// <returns></returns>
        private object GetDataSourceEdit_tabTreatmentsTypes()
        {
            return DGUIGHFData.LoadEntityFromCurrentBindingSource<treatmentstypes, DentneDModel>(_dentnedModel.TreatmentsTypes, vTreatmentsTypesBindingSource, new string[] { "treatmentstypes_id" });
        }

        /// <summary>
        /// Do actions after Save
        /// </summary>
        /// <param name="item"></param>
        private void AfterSaveAction_tabTreatmentsTypes(object item)
        {
            DGUIGHFData.SetBindingSourcePosition<treatmentstypes, DentneDModel>(_dentnedModel.TreatmentsTypes, item, vTreatmentsTypesBindingSource);
        }

        /// <summary>
        /// Add an item
        /// </summary>
        /// <param name="item"></param>
        private void Add_tabTreatmentsTypes(object item)
        {
            DGUIGHFData.Add<treatmentstypes, DentneDModel>(_dentnedModel.TreatmentsTypes, item);
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="item"></param>
        private void Update_tabTreatmentsTypes(object item)
        {
            DGUIGHFData.Update<treatmentstypes, DentneDModel>(_dentnedModel.TreatmentsTypes, item);
        }

        /// <summary>
        /// Remove an item
        /// </summary>
        /// <param name="item"></param>
        private void Remove_tabTreatmentsTypes(object item)
        {
            DGUIGHFData.Remove<treatmentstypes, DentneDModel>(_dentnedModel.TreatmentsTypes, item);
        }

        #endregion


    }
}