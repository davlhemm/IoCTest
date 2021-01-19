namespace IoCTest.Processes
{
    /// <summary>
    /// Given a continuation classifier, input, and configurable items, output an altered version
    /// </summary>
    public class ContinuationAlterProcess
    {

    }
    
    //TODO: make sure the proposed changes to continuations are decoupled from the actual write to the DB entry
    /// <summary>
    /// Shortens the "Continued on DWG" text thats in the actual model.
    /// </summary>
    /// <param name="Db"></param>
    /// <param name="excel">Blob of line list information</param>
    /// <param name="rowKey">Associated row key for drawing being used</param>
    //protected virtual void AlterContinuationText(Database Db, ExcelInfo excel, string rowKey)
    //{
    //    AlterContinuationTextLayer(Db, excel, rowKey, "TEXT");
    //}

    /// <summary>
    /// Shortens the "Continued on DWG" text thats in the actual model.
    /// </summary>
    /// <param name="Db"></param>
    /// <param name="excel">Blob of line list information</param>
    /// <param name="rowKey">Associated row key for drawing being used</param>
    /// <param name="layer">Text layer by name</param>
    //protected virtual void AlterContinuationTextLayer(Database Db, ExcelInfo excel, string rowKey, string layer)
    //{
    //    ObjectIdCollection ValidList = new ObjectIdCollection();
    //    ObjectIdCollection FixList = new ObjectIdCollection();

    //    ObjectId layerID = Db.BuildOrGetLayer(layer);

    //    ContStatus = ContinuationStatus.Updating;

    //    //Every item that needs changing...hold changelist
    //    ContChanges.Clear();

    //    try
    //    {
    //        using (BlockTable bt = Db.BlockTableId.GW<BlockTable>())
    //        {
    //            // Get writable block table record
    //            using (BlockTableRecord btr = bt.GetMSW())
    //            {
    //                // Every object in block table record
    //                foreach (ObjectId btrID in btr)
    //                {
    //                    // Get writable entity of this object
    //                    Entity item = btrID.GW<Entity>();

    //                    // If the entity is text or a block reference
    //                    if (item.GetType() == typeof(DBText) || item.GetType() == typeof(BlockReference))
    //                    {
    //                        // Add to Fix/Valid list if text entity
    //                        if (item.GetType() == typeof(DBText))
    //                        {
    //                            DBText text = item as DBText;

    //TODO: Configurable classification of Continuation
    //                            //Add to list of continuation fixes or valid text
    //                            if (text.TextString.StartsWith("CONT. FROM") ||
    //                                text.TextString.StartsWith("CONT. ON") ||
    //                                IsContStart(text.TextString))
    //                            {
    //                                FixList.Add(item.ObjectId);
    //                            }
    //                            else
    //                            {
    //                                ValidList.Add(item.ObjectId);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            BlockReference bref = item as BlockReference;
    //                            BlockTableRecord btr1 = bref.BlockTableRecord.GR<BlockTableRecord>();

    //                            bool IsText = true;

    //                            foreach (ObjectId id in btr1)
    //                            {
    //                                Entity en = id.GR<Entity>();

    //                                if (en.GetType() != typeof(DBText))
    //                                {
    //                                    IsText = false;
    //                                    break;
    //                                }
    //                            }

    //                            if (IsText)
    //                            {
    //                                ValidList.Add(item.ObjectId);
    //                            }
    //                        }
    //                    }
    //                }
    //                // Start making changes to commit
    //                using (Transaction t = Db.TransactionManager.StartTransaction())
    //                {
    //                    foreach (ObjectId oID in FixList)
    //                    {
    //                        DBText text = oID.GR<DBText>();
    //                        ObjectIdCollection keepers = new ObjectIdCollection();
    //                        Continuation cont = new Continuation();
    //                        ContinuationChange continuationChange = new ContinuationChange();

    //                        if (text.Bounds.HasValue)
    //                        {
    //                            keepers = ValidList.CheckBounds(text);

    //                            if (keepers.Count > 0)
    //                            {
    //                                cont = SortListByY(keepers);

    //                                DBText dText = new DBText();
    //                                dText = cont.Top.GW<DBText>();

    //                                if (ContExists(dText.TextString, ContSplits) &&
    //                                    !dText.TextString.Contains("EXIST."))
    //                                {
    //                                    string oldText = default, newContTest = default;
    //                                    // Build new cont string based on old one
    //                                    try
    //                                    {
    //                                        oldText = dText.TextString;
    //TODO: Port Regex Continuation String builder/alteration.  Derived from combo of item not just existing text
    //                                        newContTest = BuildContinuationString(excel, rowKey, oldText);
    //                                        dText.TextString = newContTest;
    //                                        //Continuation change was successful
    //                                        ContChanges.Add(new ContinuationChange(oldText, newContTest,
    //                                                                                ContinuationStatus.Completed));
    //                                    }
    //                                    //Done: TODO: Define what this does when it fails...it's stupid to astroturf all the continuations
    //                                    catch (System.Exception e)
    //                                    {
    //                                        // Wont: TODO: Do something...continuation potentially stayed the same
    //                                        // Add change to list but have error state
    //                                        ContChanges.Add(new ContinuationChange(oldText, newContTest,
    //                                                                                ContinuationStatus.Failed));
    //                                    }
    //                                }

    //                                #region RegEx
    //                                //if (match.Success)
    //                                //{
    //                                //    System.Text.RegularExpressions.Group g1 = match.Groups["SHEET"];
    //                                //    Capture c1 = g1.Captures[0];

    //                                //    if (dText.TextString.EndsWith(c1.Value))
    //                                //    {
    //                                //        dText.TextString = dText.TextString.Substring(0, dText.TextString.LastIndexOf(c1.Value));

    //                                //        string nf = BuildDrawingNo(dText.TextString);

    //                                //        if (!String.IsNullOrEmpty(nf))
    //                                //        {
    //                                //            dText.TextString = nf + " " + c1.Value.ToString();
    //                                //        }

    //                                //    }

    //                                //}
    //                                //else
    //                                //{
    //                                //    string nf = BuildDrawingNo(dText.TextString);

    //                                //    if (!String.IsNullOrEmpty(nf))
    //                                //    {
    //                                //        dText.TextString = nf;
    //                                //    }

    //                                //}
    //                                #endregion RegEx
    //                            }
    //                        }
    //                    }

    //TODO: Homogenize these writes/commits
    //                    t.Commit();
    //                }
    //            }
    //        }

    //        ValidList.Clear();
    //        FixList.Clear();
    //        ValidList = null;
    //        FixList = null;
    //    }
    //    catch (System.Exception)
    //    {
    //        ContStatus = ContinuationStatus.Failed;
    //        throw new System.Exception("Could not change the \"Continuation\" text. Please review the ISO.");
    //    }

    //    ContStatus = ContinuationStatus.Completed;
    //}

}