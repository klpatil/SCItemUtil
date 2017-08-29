<%@ page language="C#" autoeventwireup="true" CodeBehind="ItemUtil.aspx.cs" inherits="sc82basics.sitecore.admin.ItemUtil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Utility</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Bootstrap -->
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style type="text/css">
        /*http://getbootstrap.com/examples/navbar-fixed-top/navbar-fixed-top.css*/
        body {
            min-height: 2000px;
            padding-top: 70px;
        }
    </style>


    
</head>
<body>
    <!-- Fixed navbar -->
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">HI</a>
            </div>
            <div id="navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#">Item Util</a></li>
                </ul>
            </div>
            <!--/.nav-collapse -->
        </div>
    </nav>

    <div class="container">
        <form id="form1" runat="server" class="form-horizontal" role="form" data-toggle="validator">
            <div class="form-group">
                <label for="txtStartPath" class="col-sm-2 control-label">Start Path</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtStartPath" runat="server" placeholder="Path" CssClass="form-control" required />
                </div>
            </div>
            <div class="form-group">
                <label for="txtSourceTemplatePath" class="col-sm-2 control-label">Source Template Path</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtSourceTemplatePath" runat="server" placeholder="Source Template Path" CssClass="form-control" required />
                </div>
            </div>
            <div class="form-group">
                <label for="txtTargetTemplatePath" class="col-sm-2 control-label">Target Template Path</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtTargetTemplatePath" runat="server" placeholder="Target Template Path" CssClass="form-control" required/>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <div class="checkbox">
                        <asp:CheckBox ID="chkAllChildrens" runat="server" Text="All Childrens?" />
                    </div>
                    <div class="checkbox">
                        <asp:CheckBox ID="chkFastandFurious" runat="server" Text="Fast and Furious?" ToolTip="Would you like to disable Security/Proxy/DatabaseCache/Event/Indexing - Fastest way. But needs some manual steps once done" />
                    </div>
                </div>                
            </div>           
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:Button ID="btnStartProcessing" runat="server" Text="Start Processing" CssClass="btn btn-default" UseSubmitBehavior="true"
                         OnClick="btnStartProcessing_Click" />                    
                </div>
            </div>
            <div class="form-group">
                <label for="txtProcessResult" class="col-sm-2 control-label">Process Result</label>
                <div class="col-sm-10">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <asp:TextBox ID="txtProcessResult" runat="server" ReadOnly="true" CssClass="form-control"
                                TextMode="MultiLine" Rows="20" Columns="50" Enabled="false" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>


    <%--SCRIPTS--%>
    <script
        src="https://code.jquery.com/jquery-3.2.1.min.js"
        integrity="sha256-hwg4gsxgFZhOsEEamdOYGBf13FyQuiTwlAQgxVSNgt4="
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/1000hz-bootstrap-validator/0.11.9/validator.min.js"></script>
    <script type="text/javascript">       
        //http://1000hz.github.io/bootstrap-validator/
        $('#form1').validator().on('submit', function (e) {
            if (e.isDefaultPrevented()) {
                // handle the invalid form...
            } else {
                return confirm('Are you sure?');
            }
        })          
    </script>
</body>
</html>
