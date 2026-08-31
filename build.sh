#!/usr/bin/env bash

CONFIG="Release"
RID="linux-x64"
SINGLE_FILE="true"
SELF_CONTAINED="true"
READY_TO_RUN="false"

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
NC='\033[0m'

do_build() {
    echo -e "${CYAN}[!] Building solution...${NC}"
    dotnet build YTCDB.sln -c "$CONFIG"
}

do_test() {
    echo -e "${CYAN}[!] Running tests...${NC}"
    dotnet test YTCDB.sln -c "$CONFIG"
}

do_publish() {
    echo -e "${CYAN}[!] Publishing application for target RID: ${YELLOW}${RID}${CYAN}...${NC}"
    CMD="dotnet publish Bot/Bot.csproj -c ${CONFIG} -r ${RID} --self-contained ${SELF_CONTAINED} -o ./publish/${RID}"
    
    [ "$SINGLE_FILE" = "true" ] && CMD="$CMD /p:PublishSingleFile=true"
    [ "$READY_TO_RUN" = "true" ] && CMD="$CMD /p:PublishReadyToRun=true"

    echo -e "Executing: ${YELLOW}${CMD}${NC}\n"
    eval $CMD
}

show_menu() {
    clear
    echo -e "${CYAN}===================================================${NC}"
    echo -e "${CYAN}   YouTube Contributors Discord Bot - Build Tool   ${NC}"
    echo -e "${CYAN}===================================================${NC}"
    echo -e " Current Build Settings:"
    echo -e "   [1] Configuration   : ${YELLOW}${CONFIG}${NC}"
    echo -e "   [2] Target RID      : ${YELLOW}${RID}${NC}"
    echo -e "   [3] Single File     : ${YELLOW}${SINGLE_FILE}${NC}"
    echo -e "   [4] Self-Contained  : ${YELLOW}${SELF_CONTAINED}${NC}"
    echo -e "   [5] ReadyToRun      : ${YELLOW}${READY_TO_RUN}${NC}"
    echo -e "---------------------------------------------------"
    echo -e " Quick Target RID Selection:"
    echo -e "   [W] win-x64   [L] linux-x64   [M] osx-x64   [A] osx-arm64"
    echo -e "---------------------------------------------------"
    echo -e " Actions:"
    echo -e "   [B] Build solution"
    echo -e "   [T] Run tests"
    echo -e "   [P] Publish project"
    echo -e "   [X] Exit"
    echo -e "${CYAN}===================================================${NC}"
    read -rp "Select an option: " CHOICE
}

# Parse CLI Arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -rid)
            RID="$2"
            shift 2
            ;;
        -config)
            CONFIG="$2"
            shift 2
            ;;
        -build)
            do_build
            exit $?
            ;;
        -publish)
            do_publish
            exit $?
            ;;
        -test)
            do_test
            exit $?
            ;;
        *)
            echo -e "${RED}Unknown option: $1${NC}"
            exit 1
            ;;
    esac
done

while true; do
    show_menu
    case $CHOICE in
        1) [ "$CONFIG" = "Release" ] && CONFIG="Debug" || CONFIG="Release" ;;
        2) 
            read -rp "Enter target RID (e.g. win-x64, linux-x64, osx-x64, osx-arm64, linux-arm64): " USER_RID
            [ -n "$USER_RID" ] && RID="$USER_RID"
            ;;
        3) [ "$SINGLE_FILE" = "true" ] && SINGLE_FILE="false" || SINGLE_FILE="true" ;;
        4) [ "$SELF_CONTAINED" = "true" ] && SELF_CONTAINED="false" || SELF_CONTAINED="true" ;;
        5) [ "$READY_TO_RUN" = "true" ] && READY_TO_RUN="false" || READY_TO_RUN="true" ;;
        [wW]) RID="win-x64" ;;
        [lL]) RID="linux-x64" ;;
        [mM]) RID="osx-x64" ;;
        [aA]) RID="osx-arm64" ;;
        [bB]) clear; do_build; read -rp "Press Enter to continue..." ;;
        [tT]) clear; do_test; read -rp "Press Enter to continue..." ;;
        [pP]) clear; do_publish; read -rp "Press Enter to continue..." ;;
        [xX]) exit 0 ;;
    esac
done