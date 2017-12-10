<template>
    <span>
        <div class="no-padding">
            <div class="row no-margin">
                <div class="download-min valign-wrapper hide-on-med-and-down">

                        <a href="https://github.com/ArturGrigio/zigzag-game/blob/master/Executable/Shapeless_Windows.zip?raw=true" class="col m6 s12 center hover">
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>

                            <h6><small class="thin">DOWNLOAD FOR</small></h6>
                            <h1 class="thin no-margin">WINDOWS</h1>
                            <h3 class="no-margin"><small class="thin white-text">46.1Mb</small></h3>
                            <i class="mdi mdi-download large download-icon" style="postition: absolute;"></i>

                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                        </a>

                        <a href="https://github.com/ArturGrigio/zigzag-game/blob/master/Executable/Shapeless_Mac.zip?raw=true" class="col m6 s12 center hover">
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>

                            <h6><small class="thin">DOWNLOAD FOR</small></h6>
                            <h1 class="thin no-margin">OS X</h1>
                            <h3 class="no-margin"><small class="thin white-text">49.5Mb</small></h3>
                            <i class="mdi mdi-download large download-icon" style="postition: absolute;"></i>

                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                        </a>

                </div>

                <span class="hide-on-large-only">
                    <div class="spacer-4"></div>
                    <a href="https://github.com/ArturGrigio/zigzag-game/blob/master/Executable/Shapeless_Windows.zip?raw=true" class="col s12 center hover">
                        <h1 class="thin">WINDOWS</h1>
                        <h3><small class="thin white-text">46.1Mb</small></h3>
                        <i class="mdi mdi-download large download-icon" style="postition: absolute;"></i>
                    </a>

                    <a href="https://github.com/ArturGrigio/zigzag-game/blob/master/Executable/Shapeless_Mac.zip?raw=true" class="col s12 center hover">
                        <h1 class="thin">OS X</h1>
                        <h3><small class="thin white-text">49.5Mb</small></h3>
                        <i class="mdi mdi-download large download-icon" style="postition: absolute;"></i>
                    </a>
                </span>

            </div>
        </div>
    </span>
</template>

<style>
    .download-min {
        min-height: 300px;
    }
    .hover {
        transition: 0.5s;
    }
    .hover:hover {
        background-color: grey;
        color: grey;
    }
    .hover:hover .download-icon {
        opacity: 1;
    }
    .download-icon {
        transition: 0.25s;
        position: absolute;
        margin-left: -50px;
        color: white;
        margin-top: -180px;
        opacity: 0;
    }
</style>
