Publish self-contained image:

dotnet publish -r centos.7-x64 -c Release --self-contained -f netcoreapp2.1
dotnet publish -c Release --self-contained -f netcoreapp2.1

Make sure application listens to 0.0.0.0 inteface

Serilog - a_type={SourceContext} <- source of log


Use properties to embed somethind extra:     "Properties": {
      "Application": "Sample"
    }

Use the following to print all fields of request: {Properties} 