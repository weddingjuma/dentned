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
    public class PaymentsTypesRepository : GenericDataRepository<paymentstypes, DentneDModel>
    {
        public PaymentsTypesRepository() : base() { }

        /// <summary>
        /// Check if an item can be added
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public override bool CanAdd(ref string[] errors, params paymentstypes[] items)
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
        public override bool CanUpdate(ref string[] errors, params paymentstypes[] items)
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
        private bool Validate(bool isUpdate, ref string[] errors, params paymentstypes[] items)
        {
            bool ret = true;

            foreach (paymentstypes item in items)
            {
                if (String.IsNullOrEmpty(item.paymentstypes_name))
                {
                    ret = false;
                    errors = errors.Concat(new string[] { "Name can not be empty." }).ToArray();
                }
                if (String.IsNullOrEmpty(item.paymentstypes_doctext))
                {
                    ret = false;
                    errors = errors.Concat(new string[] { "Text can not be empty." }).ToArray();
                }

                if (!ret)
                    break;

                if (!isUpdate)
                {
                    if (List(r => r.paymentstypes_name == item.paymentstypes_name).Count() > 0)
                    {
                        ret = false;
                        errors = errors.Concat(new string[] { "Payment type already inserted." }).ToArray();
                    }
                }
                else
                {
                    if (List(r => r.paymentstypes_id != item.paymentstypes_id && r.paymentstypes_name == item.paymentstypes_name).Count() > 0)
                    {
                        ret = false;
                        errors = errors.Concat(new string[] { "Payment type already inserted." }).ToArray();
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
        public override void Add(params paymentstypes[] items)
        {
            SetIsDefault(items);

            base.Add(items);
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="items"></param>
        public override void Update(params paymentstypes[] items)
        {
            SetIsDefault(items);

            base.Update(items);
        }

        /// <summary>
        /// Set IsDefault
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private void SetIsDefault(params paymentstypes[] items)
        {
            foreach (paymentstypes item in items)
            {
                if (item.paymentstypes_isdefault)
                {
                    //unset all db items default
                    paymentstypes[] itemsupd = List().ToArray();
                    foreach (paymentstypes itemupd in itemsupd)
                    {
                        itemupd.paymentstypes_isdefault = false;
                        base.Update(itemupd);
                    }
                    //unset all current items default
                    foreach (paymentstypes item2 in items)
                    {
                        if (item2 != item)
                            item2.paymentstypes_isdefault = false;
                    }
                    break;
                }
            }
        }
    }

}

