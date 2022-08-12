#!/bin/bash

# gets the path of this bash file
SCRIPT_DIR="$( cd -- "$( dirname -- "${BASH_SOURCE[0]:-$0}"; )" &> /dev/null && pwd 2> /dev/null; )";

# removes the /App directory from the path and then adds /releases
PATH_TO_RELEASES=${SCRIPT_DIR::-4}"/release"

dotnet publish -c Release --self-contained true -r linux-x64 -p:PublishSingleFile=true -o $PATH_TO_RELEASES