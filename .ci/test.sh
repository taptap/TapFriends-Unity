# 生成测试报告 xml

resultContent=$(java -jar ./.ci/release.jar message --title="${CI_PROJECT_TITLE} Unit Test " --body="<${CI_JOB_URL}|Test Start>")
echo $resultContent
THREAD=${resultContent#*threadTs=}

cd ./TapSDK

dotnet test /p:CollectCoverage=true /p:CoverletOutput='./results/' /p:CoverletOutputFormat=opencover

# 测试模块
module=("TapBootstrapSDK")

for element in ${module[@]}
do
    dotnet reportgenerator -reports:$element/$element.Test/results/coverage.opencover.xml -targetdir:$element/$element.Test/results
    
    zip -q -ry TestReport_$element.zip $element/$element.Test/results
    
    cd ..
    
    java -jar .ci/release.jar nb --af=TapSDK/TestReport_$element.zip --thread $THREAD
    
    cd ./TapSDK
    
done
