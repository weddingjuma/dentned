﻿#region License
// Copyright (c) 2015 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System.Linq;
using DG.Data.Model;
using DG.DentneD.Model.Entity;
using System;

namespace DG.DentneD.Model.Repositories
{
    public class EstimatesFootersRepository : GenericDataRepository<estimatesfooters, DentneDModel>
    {
        public EstimatesFootersRepository() : base() { }

        /// <summary>
        /// Check if an item can be added
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public override bool CanAdd(ref string[] errors, params estimatesfooters[] items)
        {
            errors = new string[] { };

            bool ret = Validate(false, ref errors, items);
            if (!ret)
                return ret;

            ret = base.CanAdd(ref errors, items);

            return ret;
        }

        /// <summary>
        /// Check if an item can be updated
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public override bool CanUpdate(ref string[] errors, params estimatesfooters[] items)
        {
            errors = new string[] { };

            bool ret = Validate(true, ref errors, items);
            if (!ret)
                return ret;

            ret = base.CanUpdate(ref errors, items);

            return ret;
        }

        /// <summary>
        /// Validate an item
        /// </summary>
        /// <param name="isUpdate"></param>
        /// <param name="errors"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        private bool Validate(bool isUpdate, ref string[] errors, params estimatesfooters[] items)
        {
            bool ret = true;

            foreach (estimatesfooters item in items)
            {
                if (String.IsNullOrEmpty(item.estimatesfooters_name))
                {
                    ret = false;
                    errors = errors.Concat(new string[] { "Name can not be empty." }).ToArray();
                }
                if (String.IsNullOrEmpty(item.estimatesfooters_doctext))
                {
                    ret = false;
                    errors = errors.Concat(new string[] { "Text can not be empty." }).ToArray();
                }

                if (!ret)
                    break;

                if (!isUpdate)
                {
                    if (List(r => r.estimatesfooters_name == item.estimatesfooters_name).Count() > 0)
                    {
                        ret = false;
                        errors = errors.Concat(new string[] { "Estimate footer already inserted." }).ToArray();
                    }
                }
                else
                {
                    if (List(r => r.estimatesfooters_id != item.estimatesfooters_id && r.estimatesfooters_name == item.estimatesfooters_name).Count() > 0)
                    {
                        ret = false;
                        errors = errors.Concat(new string[] { "Estimate footer already inserted." }).ToArray();
                    }
                }

                if (!ret)
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Add an item
        /// </summary>
        /// <param name="items"></param>
        public override void Add(params estimatesfooters[] items)
        {
            SetIsDefault(items);

            base.Add(items);
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="items"></param>
        public override void Update(params estimatesfooters[] items)
        {
            SetIsDefault(items);

            base.Update(items);
        }

        /// <summary>
        /// Set IsDefault
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private void SetIsDefault(params estimatesfooters[] items)
        {
            foreach (estimatesfooters item in items)
            {
                if (item.estimatesfooters_isdefault)
                {
                    //unset all db items default
                    estimatesfooters[] itemsupd = List().ToArray();
                    foreach (estimatesfooters itemupd in itemsupd)
                    {
                        itemupd.estimatesfooters_isdefault = false;
                        base.Update(itemupd);
                    }
                    //unset all current items default
                    foreach (estimatesfooters item2 in items)
                    {
                        if (item2 != item)
                            item2.estimatesfooters_isdefault = false;
                    }
                    break;
                }
            }
        }
    }

}

