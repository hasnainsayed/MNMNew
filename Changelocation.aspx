<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Changelocation.aspx.cs" Inherits="Changelocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper"> <section class="content-header">
        <h1>Change Wrong Location 
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <%--<li>Report</li>--%>
            <li class="active">Change Wrong Location</li>
        </ol>
    </section>
     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
                                    <section>
                                           <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   <div runat="server" id="locdiv" visible="false">
                         <div class="row">
                             <asp:Label ID="storelocation" runat="server" Visible="false" ></asp:Label>
                           
                           <table class="table table-bordered">
                               <tr>
                                   <td>
                                       <asp:DropDownList ID="virtualLocation"  runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList>
                                   </td>
                                  
                                   <td>
                                       <asp:LinkButton ID="btngo" runat="server" CssClass="btn btn-primary pull-left" OnClick="btngo_Click" ><i class="fa fa-check-circle-o"></i> go</asp:LinkButton>
                                   </td>
                                  
                                   
                               </tr>
                               
                           </table>
                            
                                               </div>

       </div>
                        
                        <div id="chngstatus" runat="server" visible="false">
                             <div class="box-header with-border">
                        <h3 class="box-title"> <asp:Label ID="lblcurrntloc" runat="server" Visible="true" Font-Bold="true" ForeColor="#990033" ></asp:Label></h3>

                    </div>
                             <table class="table table-bordered table-stripped dtSearch">
                                <thead>
                                <tr>
                                    <th>salesid</th>
                                    <th>Username</th>
                                    <%--<th>Location</th>--%>
                                     <%--<th>Current Location</th>--%>
                                    <%--<th>Change Location</th>--%>
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rptchloc" runat="server" >
                                   <ItemTemplate>
                                       <tr>
                                           <td><asp:Label runat="server" ID="id" Visible="false" Text='<%# Eval("id").ToString() %>'></asp:Label>
                                               <asp:Label runat="server" ID="frm" Visible="false" Text='<%# Eval("frm").ToString() %>'></asp:Label>
                                               <asp:Label runat="server" ID="salesid" Visible="false" Text='<%# Eval("salesid").ToString() %>'></asp:Label>
                                               <%# Eval("salesid").ToString() %>
                                           </td>
                                           <td> <%# Eval("username").ToString() %></td>
                                               <%--<%# Eval("Location").ToString() %></td>--%>
                                           <%--<td><asp:DropDownList ID="ddllocation" runat="server" DataTextField="Location" DataValueField="saleschannelvlocid" class="form-control select2"></asp:DropDownList></td>--%>
                                          
                                           <%--<td>
                                               <asp:LinkButton ID="changelocation" runat="server" CssClass="btn btn-sm btn-reddit" OnClick="changelocation_Click" OnClientClick="return confirm('Do you Want to Change Location ?');">Change Location</asp:LinkButton></td>--%>
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>
                             
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                 <div class="col-md-4">
                            <div class="form-group">Select Correct Loction  </div>
                                                                     </div>
                                                                <div class="col-md-8">
                            <div class="form-group">
                        <asp:DropDownList ID="ddllocation" runat="server"  class="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList>
                                </div>
                                                              </div>
                    <asp:LinkButton ID="changelocation" runat="server" CssClass="btn btn-primary" OnClick="changelocation_Click" OnClientClick="return confirm('Do you Want to Change Location ?');">Change All Location</asp:LinkButton>
                                                                <asp:LinkButton ID="btnback" runat="server" CssClass="btn btn-danger" OnClick="btnback_Click">Back</asp:LinkButton>
                                                               
                                 </div>
                        </div>
                        <div id="nodata" runat="server" visible="false">
                             <div class="row">
                             
                           
                           <table class="table table-bordered">
                               <tr>
                                   <td>
                                      <h3 class="box-title" style="font:bold;color:red "> No Data</h3>
                                   </td>
                                  
                                  
                                  
                                   
                               </tr>
                               
                           </table>
                            
                                               </div>
                        </div>
                                   </div>
                    
                        </div>
                    </div>
                </div>     
                                    </section>
               



                   
</ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="changelocation" />
         </Triggers>
         </asp:UpdatePanel>
            </div>
</asp:Content>

