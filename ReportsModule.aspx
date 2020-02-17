<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportsModule.aspx.cs" Inherits="ReportsModule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
    function CallPrint(strid) {
        var headstr = "<html><head><title></title></head><body>";
  var footstr = "</body>";
  var newstr = document.all.item(strid).innerHTML;
  var oldstr = document.body.innerHTML;
  document.body.innerHTML = headstr+newstr+footstr;
  window.print();
  document.body.innerHTML = oldstr;
  return false;
    }
</script>

    <style>
       .btn-group-lg > .btn, .btn-lg {
            display: block;
        }
      .icon-size {
    font-size: 50px;
}

     .report-data {
    font-size: 15px;
    padding: 9px;
        background: aliceblue;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Reports  
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Masters</li>
                <li class="active"><a  href="ReportsModule.aspx">Reports</a></li>
               
            </ol>
    </section>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
    <ContentTemplate>
                 <div class="box box-primary" id="devCapone" runat="server" visible="true">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>

                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>--%>
                        </div>
                    <div class="box-body">
               
                         <div class="row">

                             <div class="col-md-3">
                                 <asp:Label runat="server" ID="lblreport" Visible="false"></asp:Label>
                                 <asp:Label runat="server" ID="fromDate" Visible="false"></asp:Label>
                                 <asp:Label runat="server" ID="TooDate" Visible="false"></asp:Label>
                                <div class="form-group">
                                    <%--<asp:LinkButton ID="report1" class="btn btn-primary btn-round btn-lg"  runat="server" Text="Report1" OnClick="report1_Click" ValidationGroup="grp"/>--%><%--<i class="fa-area-chart"></i>--%>
                                    <asp:LinkButton ID="report1" runat="server" CssClass="btn btn-primary btn-round btn-lg" OnClick="report1_Click" ValidationGroup="grp"><img src="dist/img/vendor-stock.png"> <br /> VendorWise Stock</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="report2" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report2" OnClick="report2_Click" ValidationGroup="grp"><i class="icon-size fa fa-database"></i> <br /> Stock</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="report3" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report3" OnClick="report3_Click" ValidationGroup="grp"><img src="dist/img/shop-stock.png"> <br /> VendorWise Stock Shop</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="report4" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report4" OnClick="report4_Click" ValidationGroup="grp"><img src="dist/img/warehouse-stock.png"> <br /> VendorWise Stock Warehouse</asp:LinkButton>
                                </div>

                            </div>
                             </div>
                        <div class="row">

                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="report5" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report5" OnClick="report5_Click" ValidationGroup="grp"><img src="dist/img/shop-margin.png"> <br /> Sales With Margin</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="report6" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report6" OnClick="report6_Click" ValidationGroup="grp"><img src="dist/img/sales-warehouse.png"> <br /> Sales For Warehouse</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="report7" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report7" OnClick="report7_Click" ValidationGroup="grp"><img src="dist/img/shop-sales.png"> <br /> Sales For Shop</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="report8" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report8" OnClick="report8_Click" ValidationGroup="grp"><img src="dist/img/purchase-margin.png"> <br />Purchase With Margin</asp:LinkButton>
                                </div>

                            </div>
                             </div>
                        <div class="row" runat="server">

                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="report9" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report9" OnClick="report9_Click" ValidationGroup="grp"><i class="icon-size fa fa-area-chart"></i> <br /> Stock LR</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3" runat="server">
                                <div class="form-group">
                                    <asp:LinkButton ID="report10" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report10" OnClick="report10_Click" ValidationGroup="grp"><i class="icon-size fa fa-bar-chart"></i> <br /> Sales With Trader Note</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3" runat="server" visible="false">
                                <div class="form-group">
                                    <asp:LinkButton ID="report11" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report11" OnClick="report11_Click" ValidationGroup="grp"><i class="icon-size fa fa-line-chart"></i> <br /> Report 11</asp:LinkButton>
                                </div>

                            </div>
                             <div class="col-md-3" runat="server" visible="false">
                                <div class="form-group">
                                    <asp:LinkButton ID="report12" class="btn btn-primary btn-round btn-lg" runat="server" Text="Report12" OnClick="report12_Click" ValidationGroup="grp"><i class="icon-size fa fa-pie-chart"></i> <br /> Report 12</asp:LinkButton>
                                </div>

                            </div>
                             </div>
                        </div>
                        </div>
                    </div>
        <div class="box box-primary" id="summid" runat="server" visible="false" >
                     <div class="box box-primary">
                    <div class="box-body">
                        <div class="row">
                            <div class="box-body table-responsive no-padding">
                                 <asp:PlaceHolder ID="PlaceHolder1"  runat="server"></asp:PlaceHolder>
                               </div>
                           </div>
                        </div>
                        </div>
            </div>

        <div class="box box-primary" id="rprtid" runat="server" >
                      <div class="box box-primary">
                          <div runat="server" id="buttons" visible="false">
                          <h3 style='padding-left:17px;'><asp:Label runat="server" ID="lblrpt"></asp:Label>
                          <asp:LinkButton ID="btnexporttoexcel" runat="server" CssClass="btn btn-success pull-right btn-round"  OnClick="btnexporttoexcel_Click"><i class="fa  fa-file-excel-o"></i> Export</asp:LinkButton>
                          <asp:Button ID="Button2" runat="server" Text="Print"  onclientclick="javascript:CallPrint('print');" class="btn btn-danger pull-right btn-round"/></h3>
                              </div>
                    <div class="box-body">
                        <div class="row">
                        <div class="box-body table-responsive no-padding" id="print">
                            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                        </div>
                         </div>
                        </div>
                    </div>
            </div>

        <asp:Panel ID="pnlDetails" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button1" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"
                    TargetControlID="Button1"
                    PopupControlID="pnlDetails"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title"><asp:Label ID="lblTitle" runat="server" Text=''></asp:Label>
                                <asp:Label ID="lblstyleIDHidden" visible="false" runat="server" Text=''></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body table-responsive" style="overflow-y:scroll;height:300px;">
                            <div>
                                <asp:label ID="rflError" Visible="false" runat="server" style="color:crimson;text-align:center;"></asp:label>
                            </div>
                        
                            <table class="table table-bordered table-striped">
                                <div class="col-md-12">
                                <h3><asp:Label runat="server">Search Box</asp:Label></h3>
                                    </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>From Date </label>
                                
                                  <asp:TextBox ID="frmDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                </div>

                            </div>

                                <div class="col-md-6">
                                <div class="form-group">
                                    <label>To Date </label>
                                
                                  <asp:TextBox ID="toDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                </div>

                            </div>
                        </table>
                            
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="btncloseViewEAN" CssClass="btn btn-sm btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup6()" />

                  <asp:LinkButton ID="btnGetRpt" runat="server" OnClick="btnGetRpt_Click" CssClass="btn btn-sm btn-success pull-right"><i class="fa fa-list"></i> Get Report</asp:LinkButton>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>
                     
    </ContentTemplate>
        <Triggers>
              <asp:PostBackTrigger ControlID="btnexporttoexcel"  />
           
        </Triggers>
    </asp:UpdatePanel>
    </div>
</asp:Content>

