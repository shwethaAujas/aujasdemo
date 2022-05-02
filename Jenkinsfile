pipeline {
    agent any

    environment {
        dotnet ='C:\\Program Files (x86)\\dotnet\\'
        MSBUILD = 'C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Msbuild\\Current\\Bin\\MSBuild.exe'
        CONFIG = 'Release'
        PLATFORM = 'x86'
        AHSCLI = 'AHSCLI\\'
        JAVA = "jre\\bin\\java"
        BINFOLDER = 'DemoHelloWorld\\HelloWorld\\bin\\x86\\Release\\net6.0\\'
        DLLS = "${BINFOLDER}*.dll"
        EXES = "${BINFOLDER}*.exe"
        INPATH = "${AHSCLI}IN\\"
        OUTPATH = "${AHSCLI}OUT\\"
    }
        
    triggers {
        pollSCM 'H * * * *'
    }

    stages {
        stage('Restore Test') {
            steps {
                bat "dotnet restore DemoHelloWorld\\HelloWorld.Test\\HelloWorld.Test.csproj"
            }
        }        
        stage('Clean Test') {
            steps {
                bat "dotnet clean DemoHelloWorld\\HelloWorld.Test\\HelloWorld.Test.csproj"
            }
        }
        stage('Build Test') {
            steps {
                bat "dotnet build DemoHelloWorld\\HelloWorld.Test\\HelloWorld.Test.csproj -c Release"
            }
        }
        stage('Run Test') {
            steps {
                bat "dotnet test DemoHelloWorld\\HelloWorld.Test\\HelloWorld.Test.csproj"
            }
        }
        stage('Restore App') {
            steps {
                bat "dotnet restore DemoHelloWorld\\HelloWorld\\HelloWorld.csproj"
            }
        }
        stage('Clean App') {
            steps {
                bat "dotnet clean DemoHelloWorld\\HelloWorld\\HelloWorld.csproj"
            }
        }
        stage('Build App') {
            steps {
                bat "dotnet build DemoHelloWorld\\HelloWorld\\HelloWorld.csproj -c Release"
            }
            post {
                always {
                    echo "Copying files"
                    bat "xcopy ${DLLS} ${INPATH} /K /D /H /Y"
                    echo "dlls copied"
                    bat "xcopy ${EXES} ${INPATH} /K /D /H /Y"
                    echo "exes copied"
                    dir("${AHSCLI}") {
                        bat "${JAVA} -Xms1g -Xmx6g -Dlog4j.configurationFile=file:log4j2.xml -cp .\\lib\\*;config.properties; com.aujas.acs.client.util.cli.CLI -guid b4e43202-3724-4752-9e48-bf3fc2b8682c -in IN\\ -out OUT\\ -sign"
                    }                    
                    echo "Code sign completed successfully"
                }
            }
        }
    }
    post {
        success {
            echo "CI/CD Pipeline executed successfully"
        }
        failure {
            echo "Fail to execute CI/CD Pipeline"
        }
    }
}
