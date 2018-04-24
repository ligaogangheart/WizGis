using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using wiz;
using System.Threading;
using System.Runtime.InteropServices;


namespace DXApplication1
{
    public partial class Form1 : RibbonForm
    {
        WIZPlayer player;
        DevExpress.XtraEditors.SplitGroupPanel wizPanel;
        SynchronizationContext mainContext;
        WIZMarkerCompoment marker;
        Dictionary<string, UInt32> modelInfos;
        WIZObliquePhotograph _wizQXSY;
        WIZGISPlatform _wizGIS;
        WIZCameraAnimation _wizCamAnim;
        WIZCameraAnimationPlay _wizCamAnimPlay;
        WIZShpCompoment _wizShpComponent;
        string _animFile;
        public Form1()
        {
            mainContext = SynchronizationContext.Current;
            InitializeComponent();
            InitSkinGallery();
            Control.CheckForIllegalCrossThreadCalls = false;
            modelInfos = new Dictionary<string, UInt32>();
        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
            wizPanel = this.splitContainerControl.Panel2;

            player = WIZPlayer.CreateInstance();

            player.addMessageHandler(WIZPlayer.Const.PMSG_INIT, this.onEngineInitHandler = this.OnEngineInited);
            player.addMessageHandler(WIZPlayer.Const.PMSG_LOADMODEL, this.onLoadModelHandler = this.OnLoadModel);
            player.addMessageHandler(WIZPlayer.Const.PMSG_REGISTERTOOL, this.onEngineToolRegisterHandler = this.OnEngineToolRegister);
            player.addMessageHandler(WIZPlayer.Const.PMSG_SELECTTOOL, this.onEngineSelectToolHandler = this.OnEngineSelectTool);

            player.addMessageHandler(WIZMarkerCompoment.Const.PMSG_EDIT, this.OnMarkerEditHander = this.OnMarkerEdit);
            player.addMessageHandler(WIZMarkerCompoment.Const.PMSG_SELECT, this.OnMarkerSelectHandler = this.OnMarkerSelect);

            player.start(wizPanel.Handle);
        }
        /// <summary>
        /// 响应Player加载模型完成消息
        /// </summary>
        /// <param name="player"></param>
        /// <param name="param"></param>
        public void OnLoadModel(WIZPlayer player, IntPtr param)
        {
            WIZPlayer.Const.ParamLoadModel st =
             Marshal.PtrToStructure<WIZPlayer.Const.ParamLoadModel>(param);

            // 几乎所有事件的回调都在Player线程中，如果我们需要进行UI操作考虑Send/Post到主线程中
            mainContext.Send((o) => { this.Text = st.fileName; }, null);
        }
        public MessageHandler onLoadModelHandler;

        /// <summary>
        /// 响应Player初始化消息
        /// </summary>
        /// <param name="player"></param>
        /// <param name="param"></param>
        public void OnEngineInited(WIZPlayer player, IntPtr param)
        {
            // Player初始化后加载所需要的插件
            string pluginDir = Application.StartupPath + "\\plugin";
            player.loadPlugins(pluginDir);
            player.setRect(0, 0, wizPanel.Width, wizPanel.Height);
            player.setFocus();

            //获取标记插件
            marker = WIZMarkerCompoment.From(player);
            if (marker.isValid())
            {
                //get error
            }

            WIZTreeCompoment tree = WIZTreeCompoment.From(player);
            if (tree.isValid())
            {
                mainContext.Post((o) =>
                {
                    tree.setParent(this.navBarControl.Handle);
                    tree.setRect(0, 0, this.navBarControl.Width, this.navBarControl.Height);
                }, null);
            }
            /// 获取组建
            _wizQXSY = WIZObliquePhotograph.From(player);
            _wizGIS = WIZGISPlatform.From(player);

            _wizCamAnim = WIZCameraAnimation.From(player);
            _wizCamAnimPlay = WIZCameraAnimationPlay.From(player);
            _wizShpComponent = WIZShpCompoment.From(player);
            if (_wizCamAnim.isValid())
            {
                player.addMessageHandler(WIZCameraAnimation.Const.PMSG_CAM_CURTIME, onCamAnimTimeChange = this.OnCamAnimTimeChange);
                //TODO: WIZCameraAnimation.Const.PMSG_CAM_DELETE

            }
        }
        private MessageHandler onEngineInitHandler; // 保存一个类成员防止委托被回收导致C++内调用失败

        public void OnCamAnimTimeChange(WIZPlayer player, IntPtr param)
        {
            int time = Marshal.PtrToStructure<int>(param);
           // mainContext.Post((o) => { this.CamTimerBar.Value = time; }, 0);
        }
        private MessageHandler onCamAnimTimeChange;
        /// <summary>
        /// 响应容纳Player的父窗体大小发生变化消息，这里我们同时通知Player做响应的大小变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WizPlane_SizeChanged(object sender, EventArgs e)
        {
            if (player.isRunning() && wizPanel != null)
            {
                var rect = wizPanel.ClientRectangle;
                player.setRect(rect.X, rect.Y, rect.Width, rect.Height);
            }
        }
        private void iHelp_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ImportModel_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                // 目前有pr(读写)和obj(读)的插件
                Filter = "模型文件|*.pr|OBJ|*.obj",
                ValidateNames = true,
                CheckPathExists = true,
                CheckFileExists = true
            };

            if (player.isRunning() && dlg.ShowDialog() == DialogResult.OK)
            {
                // 打开模型必须在Player初始化并启动后
                player.loadModel(dlg.FileName);
            }
        }
        /// <summary>
        /// 响应工具注册消息
        /// </summary>
        /// <param name="player"></param>
        /// <param name="param"></param>
        public void OnEngineToolRegister(WIZPlayer player, IntPtr param)
        {
            WIZTool tool = param;
            //TODO: add tool menu
            string name = tool.getName();
            string dname1 = tool.getDisplayName(0);
            string dname2 = tool.getDisplayName(1);
            string gname1 = tool.getGroupName(0);
            string gname2 = tool.getGroupName(1);

            mainContext.Post((o) =>
            {
                BarButtonItem baritem = new BarButtonItem();
                baritem.Caption = dname1 + '.' + dname2;
                baritem.Tag = tool;
                baritem.ItemClick += baritem_ItemClick;
                this.OperationBar.ItemLinks.Add((BarItem)baritem);
            }, null);
        }

        void baritem_ItemClick(object sender, ItemClickEventArgs e)
        {
                        BarButtonItem item = (BarButtonItem)e.Item;
            WIZTool tool = (WIZTool)item.Tag;
            player.setCurTool(tool);
        }
        private MessageHandler onEngineToolRegisterHandler;

        /// <summary>
        /// 响应Player切换工具消息
        /// </summary>
        /// <param name="player"></param>
        /// <param name="param"></param>
        public void OnEngineSelectTool(WIZPlayer player, IntPtr param)
        {
            WIZPlayer.Const.ParamSelectTool st =
                Marshal.PtrToStructure<WIZPlayer.Const.ParamSelectTool>(param);
            //TODO: 更新工具的check状态
        }
        public MessageHandler onEngineSelectToolHandler;


        public void OnMarkerEdit(WIZPlayer player, IntPtr param)
        {
            int editType = Marshal.PtrToStructure<int>(param);
            switch ((MarkerNotice)editType)
            {
                case MarkerNotice.NewMark:
                    TimeBar.Caption = "New Mark!";
                    break;
                case MarkerNotice.AddMark:
                    TimeBar.Caption = "Add Mark!";
                    break;
                case MarkerNotice.UpdateMark:
                    TimeBar.Caption = "Update Mark!";
                    break;
                case MarkerNotice.RemoveSelect:
                    TimeBar.Caption = "Remove Select!";
                    break;
                case MarkerNotice.DeleteMark:
                    TimeBar.Caption = "Delete Mark!";
                    break;
                case MarkerNotice.SelectMark:
                    TimeBar.Caption = "Select Mark!";
                    break;
                case MarkerNotice.LoadMark:
                    TimeBar.Caption = "Load Mark!";
                    break;
                case MarkerNotice.BrowseMark:
                    TimeBar.Caption = "Browse Mark!";
                    break;
                default:
                    break;
            }
        }
        public MessageHandler OnMarkerEditHander;

        public void OnMarkerSelect(WIZPlayer player, IntPtr param)
        {
            WIZMarkerCompoment.Const.ParamMarkerSelect sel =
                Marshal.PtrToStructure<WIZMarkerCompoment.Const.ParamMarkerSelect>(param);
            if (sel.bMark == 0)
            {
                siStatus.Caption = string.Format("标记:") + sel.id;
            }
            else
            {
                siStatus.Caption = string.Format("形状:") + sel.id;
            }
        }
        public MessageHandler OnMarkerSelectHandler;

        private void NewMarker_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (marker.isEdit() && marker.isEdited())
            {//如果正在编辑，提示是否放弃编辑
                string mess = string.Format("是否放弃当前");
                DialogResult a = MessageBox.Show(mess, "提示", MessageBoxButtons.YesNo);
                if (a != DialogResult.Yes)
                {
                    return;
                }
            }
            marker.newMark();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ImportMap_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                // 目前有pr(读写)和obj(读)的插件
                Filter = "*.shp",
                ValidateNames = true,
                CheckPathExists = true,
                CheckFileExists = true
            };

            if (player.isRunning() && dlg.ShowDialog() == DialogResult.OK)
            {
                // 打开模型必须在Player初始化并启动后
               WIZShpCompoment wizShpCom= WIZShpCompoment.From(player);
               wizShpCom.load(dlg.FileName);
            }
        }
        private void toolStripButtonEllipse_ItemClick(object sender, EventArgs e)
        {
            marker.selectShape(MarkerShape.Ellipse);
        }
        private void toolStripSplitButtonArray_ButtonItemClick(object sender, EventArgs e)
        {
            marker.selectShape(MarkerShape.Array);
        }
        private void toolStripSplitButtonPolygon_ButtonItemClick(object sender, EventArgs e)
        {
            marker.selectShape(MarkerShape.Polygon);
        }
        private void toolStripSplitButtonCurvel_ButtonItemClick(object sender, EventArgs e)
        {
            marker.selectShape(MarkerShape.Curvel);
        }
        private void toolStripButtonLines_ItemClick(object sender, EventArgs e)
        {
            marker.selectShape(MarkerShape.Lines);
        }
        private void toolStripSplitButtonText_ButtonItemClick(object sender, EventArgs e)
        {
            marker.selectShape(MarkerShape.Text);
        }
        private void toolStripButtonNewMark_ItemClick(object sender, EventArgs e)
        {
            if (marker.isEdit() && marker.isEdited())
            {//如果正在编辑，提示是否放弃编辑
                string mess = string.Format("是否放弃当前");
                DialogResult a = MessageBox.Show(mess, "提示", MessageBoxButtons.YesNo);
                if (a != DialogResult.Yes)
                {
                    return;
                }
            }
            marker.newMark();
        }

        private void toolStripButtonRemove_ItemClick(object sender, EventArgs e)
        {
            marker.removeCurSharp();
        }

        private void toolStripButtonSave_ItemClick(object sender, EventArgs e)
        {
            marker.saveMark();
            marker.tryEdit(false);
        }

        private void toolStripSplitButtonLineColor_ButtonItemClick(object sender, EventArgs e)
        {
            System.Random rnd = new Random();
            byte r = (byte)(rnd.Next(0, 256));
            byte g = (byte)(rnd.Next(0, 256));
            byte b = (byte)(rnd.Next(0, 256));
            byte a = (byte)(rnd.Next(0, 256));
            marker.setLineColor(r, g, b, a);
        }

        private void toolStripSplitButtonFillColor_ButtonItemClick(object sender, EventArgs e)
        {
            System.Random rnd = new Random();
            byte r = (byte)(rnd.Next(0, 256));
            byte g = (byte)(rnd.Next(0, 256));
            byte b = (byte)(rnd.Next(0, 256));
            byte a = (byte)(rnd.Next(0, 256));
            marker.setFillColor(r, g, b, a);
        }

        private void toolStripSplitButtonLineWidth_ButtonItemClick(object sender, EventArgs e)
        {
            //int width = 5;
            System.Random rnd = new Random();
            int width = rnd.Next(3, 21);
            marker.setLineWidth(width);
        }

        private void toolStripSplitButtonBrouse_ButtonItemClick(object sender, EventArgs e)
        {
            //回到浏览模式
            bool bEdit = marker.isEdit();
            bool bEdited = marker.isEdited();
            bool bSave = false;
            if (bEdit && bEdited)
            {//编辑状态且标记已经被修改 
                //提示是否保存标记
                string mess = string.Format("检测到有标记被编辑，是否保存");
                DialogResult a = MessageBox.Show(mess, "提示", MessageBoxButtons.YesNo);
                if (a == DialogResult.Yes)
                {
                    bSave = true;
                }
            }
            if (bSave)
            {
                marker.saveMark();
            }
            marker.tryEdit(false);
        }
        private void toolStripButtonDeleteMark_ItemClick(object sender, EventArgs e)
        {
            //彻底删除当前标记
            marker.deleteCurMark();
        }

        private void toolStripComboBoxModelFile_ItemClick(object sender, EventArgs e)
        {
        }

        private void Rect_ItemClick(object sender, ItemClickEventArgs e)
        {
            marker.selectShape(MarkerShape.Rect);
        }

        private void loadLayerButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            WIZIGeometry geo = _wizGIS.loadGeometry(@"F:\test\obj.V3FN4BX");
            for (int x = 0; x < 4; ++x)
            {
                for (int y = 0; y < 2; ++y)
                {
                    real2 longlat = new real2();
                    longlat.x = 116 + x * 0.1;
                    longlat.y = 40 + y * 0.1;
                    nativeWIZGISPlatform.longlatToWorld(ref longlat);

                    WIZNode node = geo.createNode();
                    float3 pos = new float3((float)longlat.x, (float)longlat.y, 0);
                    node.setPosition(pos);

                    pos.x = 500;
                    pos.y = 500;
                    pos.z = 500;

                    node.setScale(pos);
                    pos.x = 0;
                    pos.y = 0;
                    pos.z = 1;

                    node.setAngle(0, pos);

                    node.updateAabb();
                }
            } 
        }

        private void MultiRect_ItemClick(object sender, ItemClickEventArgs e)
        {
            marker.selectShape(MarkerShape.Polygon);
        }

        private void RemoveCurrent_ItemClick(object sender, ItemClickEventArgs e)
        {
            marker.removeCurSharp();
        }

        private void BackColor_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Browser_ItemClick(object sender, ItemClickEventArgs e)
        {
            //回到浏览模式
            bool bEdit = marker.isEdit();
            bool bEdited = marker.isEdited();
            bool bSave = false;
            if (bEdit && bEdited)
            {//编辑状态且标记已经被修改
                //提示是否保存标记
                string mess = string.Format("检测到有标记被编辑，是否保存");
                DialogResult a = MessageBox.Show(mess, "提示", MessageBoxButtons.YesNo);
                if (a == DialogResult.Yes)
                {
                    bSave = true;
                }
            }
            if (bSave)
            {
                marker.saveMark();
            }
            marker.tryEdit(false);
        }

        private void Curve_ItemClick(object sender, ItemClickEventArgs e)
        {
            marker.selectShape(MarkerShape.Curvel);
        }

        private void save_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void testdemobutton_ItemClick(object sender, ItemClickEventArgs e)
        {
            quexian quexianwindow = new quexian();
            quexianwindow.ShowDialog();
        }

        private void wizardPage_ItemClick(object sender, ItemClickEventArgs e)
        {
            _wizShpComponent.load(@"F:\test\ocldnta.shp");
        }

        private void LoadPageBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            floatPage page = new floatPage();
            page.ShowDialog();
        }
    }
}