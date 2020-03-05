<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="设备台账.aspx.cs" Inherits="FineUIPro.EmptyProjectNet40.设备台账.设备台账" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .element.style {
            width: 300px;
            height: 200px;
        }

        f-field-body f-field-label {
            width: 300px;
            height: 200px;
        }

        .photo img {
            height: 250px;
            vertical-align: middle;
            text-align: center;
            max-width: 70%;
            stroke-width: inherit;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel7" runat="server" />
        <f:Panel ID="Panel7" runat="server" BodyPadding="10px"
            Title="Panel" ShowBorder="false" ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Form ID="Form2" ShowBorder="False" ShowHeader="False" runat="server">
                    <Rows>
                        <f:FormRow ColumnWidths="20% 80%">
                            <Items>
                                <f:Tree ID="Tree1" IsFluid="true" AutoScroll="true" CssClass="blockpanel" ShowHeader="true" Title="设备类型" EnableCollapse="false"
                                    runat="server" Height="900px" EnableSingleExpand="true" EnableCheckBox="false" AutoLeafIdentification="true" OnNodeCommand="Tree1_NodeCommand">
                                </f:Tree>
                                <%--  <f:Tree runat="server" ShowBorder="false" ShowHeader="false" ID="treeMenu" EnableSingleClickExpand="true"
                            HeaderStyle="false" HideHScrollbar="false" HideVScrollbar="false" ExpanderToRight="false"> --%>

                                <f:Grid ID="Grid1" Title="数据表格" PageSize="15" IsFluid="true" EnableCheckBoxSelect="true" ShowBorder="true" BoxFlex="1" AllowPaging="true" IsDatabasePaging="false" ShowHeader="true" runat="server" DataKeyNames="ID,SBID,SAP编号,设备编号,设备名称,设备规格,设备型号,投产时间,部门名称,单位名称,使用单位" OnRowCommand="Grid1_RowCommand">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                                </f:ToolbarSeparator>
                                                <f:TwinTriggerBox runat="server" EmptyText="输入要搜索的关键词" ShowLabel="false" ID="ttbSearch" OnTrigger2Click="ttbSearch_Trigger2Click"
                                                    ShowTrigger1="false"
                                                    Trigger1Icon="Clear" Trigger2Icon="Search" Width="300">
                                                </f:TwinTriggerBox>
                                                <f:Label runat="server" ID="储存ID" Hidden="true"></f:Label>
                                                <f:Label runat="server" ID="储存rank" Hidden="true"></f:Label>

                                                <f:Button runat="server" ID="multiSelect" IconAlign="Right" OnClick="multiSelect_Click" Text="多选"></f:Button>
                                                <f:Button runat="server" ID="cancelMultiSelect" IconAlign="Right" OnClick="cancelMultiSelect_Click" Hidden="true" Text="取消多选"></f:Button>
                                                <f:Button runat="server" ID="multiSelectPpload" Hidden="true" OnClick="multiSelectPpload_Click" Text="上传"></f:Button>


                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RowNumberField />
                                        <f:RenderField ColumnID="ID" DataField="ID" HeaderText="ID" Hidden="True"></f:RenderField>
                                        <f:RenderField ColumnID="SBID" DataField="SBID" HeaderText="SBID" Hidden="True"></f:RenderField>
                                        <f:RenderField ColumnID="SAP编号" DataField="SAP编号" HeaderText="SAP编号" Hidden="True"></f:RenderField>
                                        <f:RenderField ColumnID="设备编号" DataField="设备编号" HeaderText="设备编号"></f:RenderField>
                                        <f:RenderField ColumnID="设备名称" DataField="设备名称" HeaderText="设备名称" Width="200"></f:RenderField>
                                        <f:RenderField ColumnID="设备型号" DataField="设备型号" HeaderText="设备型号" />
                                        <f:RenderField ColumnID="固资原值" DataField="固资原值" HeaderText="固资原值" Hidden="true" />
                                        <f:RenderField ColumnID="固资净值" DataField="固资净值" HeaderText="固资净值" Hidden="true" />
                                        <f:RenderField ColumnID="设备规格" DataField="设备规格" HeaderText="设备规格" />
                                        <f:RenderField ColumnID="制造商" DataField="制造商" HeaderText="制造商" Width="300" ExpandUnusedSpace="true" />
                                        <f:RenderField ColumnID="投产时间" DataField="投产时间" HeaderText="投产时间" Hidden="true" />
                                        <f:RenderField ColumnID="部门名称" DataField="部门名称" HeaderText="部门名称" Hidden="True" />
                                        <f:RenderField ColumnID="单位名称" DataField="单位名称" HeaderText="单位名称" Hidden="True" />
                                        <f:RenderField ColumnID="使用单位" DataField="使用单位" HeaderText="使用单位" Hidden="True"></f:RenderField>
                                        <f:LinkButtonField Text="上传图片" ID="uppictures" Width="120px" TextAlign="Center" CommandName="UpPictures"></f:LinkButtonField>
                                        <f:LinkButtonField Text="查看图片" ID="selectpictures" Width="120px" TextAlign="Center" CommandName="SelectPictures"></f:LinkButtonField>
                                        <f:LinkButtonField Text="上传文件" ID="uploadfileInternet" Width="120px" TextAlign="Center" Hidden="false" CommandName="UploadFiles"></f:LinkButtonField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <%-- 图片预览 --%>
                <f:Window ID="window1" Title="图片预览" runat="server" Hidden="true" EnableIFrame="false"
                    EnableMaximize="true" Target="Self" EnableResize="false" IsModal="true" Width="590px" Height="480px">
                    <Items>
                        <f:Image ID="image" runat="server" Margin="20px" ImageHeight="400px" ImageWidth="550px"></f:Image>
                    </Items>
                </f:Window>
                <%-- 文件上传 --%>
                <f:Window ID="window2" Title="文件上传" Hidden="true" EnableIFrame="false" EnableMaximize="true" CloseAction="HidePostBack" OnClose="window2_Close" Target="Self" EnableResize="false" IsModal="true" Width="760px" Height="300px" runat="server">
                    <Items>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:FileUpload runat="server" ID="uploadDocument" Width="400px" EmptyText="请选择文件" Label="文件上传" LabelWidth="130" Required="true" ButtonIcon="Add"
                                    ShowRedStar="true">
                                </f:FileUpload>
                                <%--<f:DropDownList Label="上传条件" ID="上传条件" EmptyText="请选择所要上传上传条件" OnSelectedIndexChanged="上传条件_SelectedIndexChanged" runat="server">
                                    <f:ListItem Text="设备文件类型" Value="filter1" />
                                </f:DropDownList>--%>
                            </Items>
                        </f:Toolbar>
                        <%--<f:Toolbar runat="server">
                             <Items>
                                <f:Label Text="上传类型" Width="100px" runat="server"></f:Label>
                                <f:Button Text="设备文件类型" ID="button1" OnClick="button1_Click" runat="server"></f:Button>
                            </Items>
                        </f:Toolbar>--%>
                        <f:Toolbar CssStyle="border:unset;" runat="server">
                            <Items>
                                <f:Label Text="上传类型" runat="server"></f:Label>
                                <f:DropDownList ID="上传类型" ShowLabel="false" AutoPostBack="true" CssStyle="border:white" OnSelectedIndexChanged="上传类型_SelectedIndexChanged" runat="server">
                                    <f:ListItem Text="全部" Value="全部" />
                                    <f:ListItem Text="设备文件类型" Value="设备文件类型" />
                                    <f:ListItem Text="设备台账类型" Value="设备台账类型" />
                                    
                                </f:DropDownList>
                                <f:DropDownList ID="设备台账类型" Width="250px" Label="设备台账类型" Hidden="true" LabelWidth="110" AutoPostBack="true" OnSelectedIndexChanged="设备台账类型_SelectedIndexChanged" AutoSelectFirstItem="false" runat="server">
                                </f:DropDownList>
                                <f:DropDownList ID="设备文件类型" Width="250px" Label="设备文件类型" Hidden="true" LabelWidth="110" AutoPostBack="true" OnSelectedIndexChanged="设备文件类型_SelectedIndexChanged" AutoSelectFirstItem="false" runat="server">
                                </f:DropDownList>

                                <%--<f:DropDownList Label="设备文件类型" runat="server"></f:DropDownList>--%>
                            </Items>
                        </f:Toolbar>
                    </Items>
                    <Toolbars>
                        <f:Toolbar Position="Bottom" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:Button ID="Button3" IconUrl="../res/icon/accept.png" runat="server" OnClick="Button3_Click" Text="上传"></f:Button>
                                <f:Button ID="Button4" runat="server" Text="关闭" IconUrl="../res/icon/cancel.png" OnClick="Button4_Click" RegionPosition="Right"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Window>

                <%-- 上传图片 --%>
                <f:Window ID="window3" Title="上传图片" runat="server" Hidden="true" CloseAction="HidePostBack" OnClose="window2_Close" EnableIFrame="false"
                    EnableMaximize="true" Target="Self" EnableResize="true" IsModal="true" Width="660px" Height="360px">
                    <Items>
                        <f:ContentPanel runat="server" IsFluid="true" BodyPadding="10" Title="" CssStyle="font-size:12px;Padding-Top:0px;" Height="550px" ShowHeader="false" ShowBorder="true">
                            <div id="previewaa">
                                <f:Image ID="imgPhoto" CssClass="photo" ImageUrl="../res/images/blank.png" ShowEmptyLabel="true" runat="server">
                                </f:Image>
                            </div>

                        </f:ContentPanel>
                    </Items>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar4" Position="Bottom" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:FileUpload runat="server" ID="upFile" ShowRedStar="false" ShowEmptyLabel="true"
                                    ButtonText="选择图片" ButtonOnly="true" Required="false" ButtonIcon="ImageAdd"
                                    AutoPostBack="true" OnFileSelected="filePhoto_FileSelected">
                                </f:FileUpload>
                                <f:Button ID="Button2" IconUrl="../res/icon/accept.png" runat="server" OnClick="Button2_Click" Text="上传"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Window>
                <%--多选上传图片--%>
                <f:Window ID="window4" Title="上传文件" Hidden="true" AutoScroll="true" CloseAction="HidePostBack" OnClose="window4_Close" EnableIFrame="false"
                    EnableMaximize="true" Target="Self" EnableResize="true" IsModal="true" Width="660px" runat="server">
                    <Items>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:FileUpload runat="server" ID="uploadDocument2" Width="400px" EmptyText="请选择文件" Label="文件上传" LabelWidth="130" Required="true" ButtonIcon="Add"
                                    ShowRedStar="true">
                                </f:FileUpload>
                                <%--<f:DropDownList Label="上传条件" ID="上传条件" EmptyText="请选择所要上传上传条件" OnSelectedIndexChanged="上传条件_SelectedIndexChanged" runat="server">
                                    <f:ListItem Text="设备文件类型" Value="filter1" />
                                </f:DropDownList>--%>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar CssStyle="border:unset;" runat="server">
                            <Items>
                                <f:Label Text="上传类型" runat="server"></f:Label>
                                <f:DropDownList ID="DropDownList1" ShowLabel="false" AutoPostBack="true" CssStyle="border:white" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server">
                                    <f:ListItem Text="全部" Value="全部" />
                                    <f:ListItem Text="设备文件类型" Value="设备文件类型" />
                                    <f:ListItem Text="设备台账类型" Value="设备台账类型" />
                                </f:DropDownList>
                                <f:DropDownList ID="DropDownList2" Width="250px" Label="设备台账类型" Hidden="true" LabelWidth="110" AutoPostBack="true" OnSelectedIndexChanged="设备台账类型_SelectedIndexChanged" AutoSelectFirstItem="false" runat="server">
                                </f:DropDownList>
                                <f:DropDownList ID="DropDownList3" Width="250px" Label="设备文件类型" Hidden="true" LabelWidth="110" AutoPostBack="true" OnSelectedIndexChanged="设备文件类型_SelectedIndexChanged" AutoSelectFirstItem="false" runat="server">
                                </f:DropDownList>

                                <%--<f:DropDownList Label="设备文件类型" runat="server"></f:DropDownList>--%>
                            </Items>
                        </f:Toolbar>
                        <f:Grid ID="Grid2" IsFluid="true" CssClass="span" ShowBorder="true" ShowHeader="true" AllowPaging="true" IsDatabasePaging="false" PageSize="10" Title="表格" EnableCollapse="false"
                            runat="server" AllowCellEditing="true" ClicksToEdit="2" DataIDField="ID" EnableCheckBoxSelect="false">
                            <Columns>
                                <f:RenderField ColumnID="ID" DataField="ID" HeaderText="ID" Hidden="True"></f:RenderField>
                                <f:RenderField ColumnID="SBID" DataField="SBID" HeaderText="SBID" Hidden="True"></f:RenderField>
                                <f:RenderField ColumnID="SAP编号" DataField="SAP编号" HeaderText="SAP编号" Hidden="True"></f:RenderField>
                                <f:RenderField ColumnID="设备编号" DataField="设备编号" HeaderText="设备编号"></f:RenderField>
                                <f:RenderField ColumnID="设备名称" DataField="设备名称" HeaderText="设备名称" Width="200"></f:RenderField>
                                <f:RenderField ColumnID="设备型号" DataField="设备型号" HeaderText="设备型号" />
                                <f:RenderField ColumnID="固资原值" DataField="固资原值" HeaderText="固资原值" Hidden="true" />
                                <f:RenderField ColumnID="固资净值" DataField="固资净值" HeaderText="固资净值" Hidden="true" />
                                <f:RenderField ColumnID="设备规格" DataField="设备规格" HeaderText="设备规格" />
                                <f:RenderField ColumnID="制造商" DataField="制造商" HeaderText="制造商" Width="300" ExpandUnusedSpace="true" />
                                <f:RenderField ColumnID="投产时间" DataField="投产时间" HeaderText="投产时间" Hidden="true" />
                                <f:RenderField ColumnID="部门名称" DataField="部门名称" HeaderText="部门名称" Hidden="True" />
                                <f:RenderField ColumnID="单位名称" DataField="单位名称" HeaderText="单位名称" Hidden="True" />
                                <f:RenderField ColumnID="使用单位" DataField="使用单位" HeaderText="使用单位" Hidden="True"></f:RenderField>
                            </Columns>
                        </f:Grid>
                    </Items>
                    <Toolbars>
                        <f:Toolbar Position="Bottom" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:Button ID="Button5" IconUrl="../res/icon/accept.png" OnClick="Button5_Click" runat="server" Text="上传"></f:Button>
                                <f:Button ID="Button6" runat="server" Text="关闭" IconUrl="../res/icon/cancel.png" OnClick="Button6_Click" RegionPosition="Right"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Window>
                <f:Window ID="window5" Title="提示" Hidden="true" AutoScroll="true" CloseAction="HidePostBack" OnClose="window5_Close1" EnableIFrame="false"
                    EnableMaximize="true" Target="Self" EnableResize="false" IsModal="true" Height="260px" Width="300px" runat="server">
                    <Items>
                        <f:Label runat="server" Text="此设备已上传过文件，是否替换此文件？" Margin="50px"></f:Label>
                    </Items>
                    <Toolbars>                        
                        <f:Toolbar Position="Bottom" ToolbarAlign="Right" runat="server">
                            <Items>
                                <f:Button ID="replace" IconUrl="../res/icon/accept.png" OnClick="replace_Click" runat="server" Text="替换"></f:Button>
                                <f:Button ID="Button7" runat="server" Text="取消" IconUrl="../res/icon/cancel.png" OnClick="Button7_Click" RegionPosition="Right"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Window>
            </Items>
        </f:Panel>

    </form>
</body>
</html>
