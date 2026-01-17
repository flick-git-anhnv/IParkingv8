using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Text;

namespace Futech.Objects
{
    public class Config
    {
        // Nhom doi tuong
        private LineTypeCollection linetypes = null;
        // Doi tuong
        private ControllerTypeCollection controllertypes = null;

        // File luu tru doi tuong
        private string objectsfile = "";

        // Ham tao
        public Config()
        {

        }

        // Ham huy
        public void Dispose()
        {

        }

        // Luu danh sach nhom doi tuong
        public void SaveObjects(ref LineTypeCollection _linetypes, ref ControllerTypeCollection _controllertypes, string _objectsfile)
        {
            try
            {
                linetypes = _linetypes;
                controllertypes = _controllertypes;
                objectsfile = _objectsfile;

                // open file
                FileStream fs = new FileStream(objectsfile, FileMode.Create);
                // create XML writer
                XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.UTF8);

                // use indenting for readability
                xmlOut.Formatting = Formatting.Indented;

                // start document
                xmlOut.WriteStartDocument();

                // main node
                xmlOut.WriteStartElement("Objects");

                // save all objects
                SaveObjects(xmlOut, null);

                // close "Objects" node
                xmlOut.WriteEndElement();

                // close file
                xmlOut.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Luu danh sach nhom doi tuong\n" + ex.Message);
            }
        }

        // Luu danh sach doi tuong
        private void SaveObjects(XmlTextWriter writer, LineType parent)
        {
            foreach (LineType linetype in linetypes)
            {
                if (linetype.Parent == parent)
                {
                    // new "LineType" node
                    writer.WriteStartElement("LineType");
                    
                    // write node
                    writer.WriteAttributeString("ID", linetype.ID.ToString());
                    writer.WriteAttributeString("Name", linetype.Name);

                    // write child groups
                    SaveObjects(writer, linetype);

                    // close "LineType" node
                    writer.WriteEndElement();
                }
            }
            foreach (ControllerType controllertype in controllertypes)
            {
                if (controllertype.Parent == parent)
                {
                    // new "ControllerType" node
                    writer.WriteStartElement("ControllerType");

                    // write node
                    writer.WriteAttributeString("ID", controllertype.ID.ToString());
                    writer.WriteAttributeString("Name", controllertype.Name);
                    writer.WriteAttributeString("LineTypeID", controllertype.LineTypeID.ToString());

                    // close "ControllerType" node
                    writer.WriteEndElement();
                }
            }
        }

        // Lay du lieu tu danh sach doi tuong
        public void LoadObjects(ref LineTypeCollection _linetypes, ref ControllerTypeCollection _controllertypes, string _objectsfile)
        {
            linetypes = _linetypes;
            controllertypes = _controllertypes;
            objectsfile = _objectsfile;

            // check file existance
            if (File.Exists(objectsfile))
            {
                FileStream fs = null;
                XmlTextReader xmlIn = null;

                try
                {
                    linetypes = _linetypes;
                    controllertypes = _controllertypes;
                    objectsfile = _objectsfile;
                    // open file
                    fs = new FileStream(objectsfile, FileMode.Open);
                    // create XML reader
                    xmlIn = new XmlTextReader(fs);

                    xmlIn.WhitespaceHandling = WhitespaceHandling.None;
                    xmlIn.MoveToContent();

                    // check for main node
                    if (xmlIn.Name != "Objects")
                        throw new ApplicationException("");

                    // move to next node
                    xmlIn.Read();
                    if (xmlIn.NodeType == XmlNodeType.EndElement)
                        xmlIn.Read();

                    // Lay du lieu tu danh sach doi tuong
                    LoadObjects(xmlIn, null);
                }
                // catch any exceptions
                catch (Exception ex)
                {
                    MessageBox.Show("Lay du lieu tu danh sach doi tuong\n" + ex.Message);
                }
                finally
                {
                    if (xmlIn != null)
                        xmlIn.Close();
                }
            }
        }

        // Load objects
        private void LoadObjects(XmlTextReader reader, LineType parent)
        {
            // load all linetypes
            while (reader.Name == "LineType")
            {
                int depth = reader.Depth;

                // create new linetype
                LineType linetype = new LineType();
                linetype.ID = int.Parse(reader.GetAttribute("ID"));
                linetype.Name = reader.GetAttribute("Name");
                linetype.Parent = parent;

                // add linetype
                linetypes.Add(linetype);

                // move to next node
                reader.Read();

                // move to next element node
                while (reader.NodeType == XmlNodeType.EndElement)
                    reader.Read();
                // read children
                if (reader.Depth > depth)
                    LoadObjects(reader, linetype);
                //
                if (reader.Depth < depth)
                    return;
            }
            // load all objects
            while (reader.Name == "ControllerType")
            {
                int depth = reader.Depth;

                // create new controllertype
                ControllerType controllertype = new ControllerType();
                controllertype.ID = int.Parse(reader.GetAttribute("ID"));
                controllertype.Name = reader.GetAttribute("Name");
                controllertype.LineTypeID = int.Parse(reader.GetAttribute("LineTypeID"));
                controllertype.Parent = parent;

                // add controllertype
                controllertypes.Add(controllertype);

                // move to next node
                reader.Read();

                // move to next element node
                while (reader.NodeType == XmlNodeType.EndElement)
                    reader.Read();
                if (reader.Depth < depth)
                    return;
            }
        }
    }
}

 