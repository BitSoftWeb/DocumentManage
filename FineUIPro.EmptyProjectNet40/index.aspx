<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FineUIPro.EmptyProjectNet40.index" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>FineUIPro 空项目</title>
    <link href="~/res/css/index.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"></f:PageManager>
        <f:Panel ID="Panel1" Layout="Region" CssClass="mainpanel" ShowBorder="false" ShowHeader="false" runat="server">
            <Items>
                <f:ContentPanel ID="topPanel" CssClass="topregion bgpanel" RegionPosition="Top" ShowBorder="false" ShowHeader="false" EnableCollapse="true" runat="server">
                    <div id="header" class="f-widget-header f-mainheader">
                        <table>
                            <tr>
                                <td>
                                    <f:Button runat="server" CssClass="icononlyaction" ID="btnHomePage" ToolTip="官网首页" IconAlign="Top" IconFont="_Home"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false"
                                        OnClientClick="window.open('http://fineui.com/pro/','_blank');">
                                    </f:Button>
                                    <a class="logo" href="./index.aspx">资产文档协同管理系统
                                    </a>
                                </td>
                                <td style="text-align: right;">
                                   <%-- <f:Button runat="server" CssClass="icontopaction themes" ID="btnThemeSelect" Text="主题仓库" IconAlign="Top" IconFont="_Skin"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false">
                                        <Listeners>
                                            <f:Listener Event="click" Handler="onThemeSelectClick" />
                                        </Listeners>
                                    </f:Button>--%>
                                    <f:Button runat="server" ID="btn1" CssClass="userpicaction"  IconUrl="~/res/images/12112ftgtt.png" IconAlign="Left"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false">
                                        <Menu runat="server">
                                            <f:MenuButton Text="个人信息" IconFont="_User" EnablePostBack="false" runat="server">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onUserProfileClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                            <f:MenuSeparator runat="server"></f:MenuSeparator>
                                            <f:MenuButton Text="安全退出" IconFont="_SignOut" EnablePostBack="false" runat="server">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onSignOutClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                        </Menu>
                                    </f:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin-left:15px;padding-top:15px;padding-bottom:10px;">
                        <table>
                            <tr>
                               <%-- <td>                                   
                                    <f:Button runat="server" ID="Button1" OnClientClick="Repair_People_Analyze()" Text="查询文件"></f:Button>
                                </td>--%>
                                <td>                                   
                                    <f:Button runat="server" ID="Button2" OnClientClick="Repair_People_Analyze()" Text="文件上传"></f:Button>
                                </td>
                                <td>                                    
                                    <f:Button runat="server" ID="Button3" OnClientClick="docDownload()" Text="文件下载"></f:Button>
                                </td>
                                <td>
                                    <f:Button runat="server" ID="Button4" OnClientClick="file_Operation_Records()" Text="文件操作记录"></f:Button>
                                </td>

                            </tr>
                        </table>
                    </div>
                </f:ContentPanel>

                <f:Panel ID="leftPanel" CssClass="leftregion bgpanel" Width="220px" Hidden="true" ShowHeader="false" Title="菜单"
                    EnableCollapse="true" Layout="Fit" RegionPosition="Left"
                    RegionSplit="true" RegionSplitWidth="3" RegionSplitIcon="false" runat="server">
                    <Items>
                        <f:Tree runat="server" ShowBorder="false" ShowHeader="false" ID="treeMenu" EnableSingleClickExpand="true"
                            HeaderStyle="false" HideHScrollbar="false" HideVScrollbar="false" ExpanderToRight="false">
                            <%--<Nodes>
                                <f:TreeNode Text="设备台账" Expanded="false">
                                    <f:TreeNode Text="设备台账" IconUrl="res/icon/arrow_switch.png" NavigateUrl="设备台账/设备台账.aspx"></f:TreeNode>
                                </f:TreeNode>
                                 <f:TreeNode Text="设备文件表">
                                    <f:TreeNode Text="设备文件" IconUrl="res/icon/arrow_switch.png" NavigateUrl="设备文件/设备文件.aspx"></f:TreeNode>
                                </f:TreeNode>
                                <f:TreeNode Text="设备更新文件" Expanded="false">
                                    <f:TreeNode Text="设备更新" IconUrl="res/icon/arrow_switch.png" NavigateUrl="~/hello.aspx"></f:TreeNode>                           
                                </f:TreeNode>                                                           
                            </Nodes>--%>
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel ID="mainPanel" CssClass="centerregion" ShowHeader="false" Layout="Fit" RegionPosition="Center" runat="server">
                    <Items>
                        <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server" ShowInkBar="true">
                            <Tabs>
                                
                                <f:Tab ID="Tab1" Title="首页" BodyPadding="10px" EnableIFrame="false" AutoScroll="true" Icon="House" runat="server">
                                </f:Tab>
                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

        <%--<f:Window ID="windowThemeRoller" Title="主题仓库" Hidden="true" EnableIFrame="true" IFrameUrl="./common/themes.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1020px" Height="600px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>--%>
    </form>
    <script>
        var treeMenuClientID = '<%= treeMenu.ClientID %>';
        var mainTabStripClientID = '<%= mainTabStrip.ClientID %>';
       <%-- var windowThemeRollerClientID = '<%= windowThemeRoller.ClientID %>';--%>


        function Repair_People_Analyze()
        {
            parent.addExampleTab({
                id: 'Repairpeople',
                iframeUrl: '设备台账/设备台账.aspx',
                title: '文件上传',
                icon: 'res/icon/book_edit.png',
                refreshWhenExist: true
            });
        }

        function docDownload()
        {
             parent.addExampleTab({
                id: '文件下载',
                iframeUrl: '设备文件/设备文件.aspx',
                title: '文件下载',
                icon: 'res/icon/arrow_switch.png',
                refreshWhenExist: true
            });
        }
        
        function file_Operation_Records()
        {
             parent.addExampleTab({
                id: 'file_Operation_Records',
                iframeUrl: '文件操作记录/FileOperationRecords.aspx',
                title: '文件操作记录',
                 icon: 'res/icon/folder_up.png',
                refreshWhenExist: true
            });
        }
        


        // 点击主题仓库
        function onThemeSelectClick(event) {
            F(windowThemeRollerClientID).show();
        }

        function onUserProfileClick(event) {
            F.alert('尚未实现');
        }

        function onSignOutClick(event) {
            F.alert('尚未实现');
        }
        function addExampleTab(tabOptions, actived) {

            if (typeof (tabOptions) === 'string') {
                tabOptions = {
                    id: arguments[0],
                    iframeUrl: arguments[1],
                    title: arguments[2],
                    icon: arguments[3],
                    createToolbar: arguments[4],
                    refreshWhenExist: arguments[5],
                    iconFont: arguments[6]
                };
            }

            F.addMainTab(F(mainTabStripClientID), tabOptions, actived);
        }

        // 页面控件初始化完毕后执行
        F.ready(function () {
            var treeMenu = F(treeMenuClientID);
            var mainTabStrip = F(mainTabStripClientID);
            if (!treeMenu) return;

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // updateHash: 切换Tab时，是否更新地址栏Hash值（默认值：true）
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame（默认值：false）
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame（默认值：false）
            // maxTabCount: 最大允许打开的选项卡数量
            // maxTabMessage: 超过最大允许打开选项卡数量时的提示信息
            F.initTreeTabStrip(treeMenu, mainTabStrip, {
                maxTabCount: 10,
                maxTabMessage: '请先关闭一些选项卡（最多允许打开 10 个）！'
            });

        });
    </script>
</body>
</html>
