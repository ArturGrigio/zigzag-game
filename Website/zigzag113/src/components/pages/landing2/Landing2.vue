<template>
    <span>
        <div class="container section no-padding">
            <div class="row no-margin">
                <div class="download-min valign-wrapper hide-on-med-and-down">

                        <a href="https://github.com/ArturGrigio/zigzag-game/blob/master/Executable/Shapeless_Windows.zip?raw=true" class="col m6 s12 center hover">
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>
                            <div class="spacer-4"></div>

                            <h1 class="thin no-margin">WINDOWS</h1>
                            <h3 class="no-margin"><small class="thin white-text">46.1Mb</small></h3>

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

                            <h1 class="thin no-margin">OS X</h1>
                            <h3 class="no-margin"><small class="thin white-text">49.5Mb</small></h3>

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
                    </a>

                    <a href="https://github.com/ArturGrigio/zigzag-game/blob/master/Executable/Shapeless_Mac.zip?raw=true" class="col s12 center hover">
                        <h1 class="thin">OS X</h1>
                        <h3><small class="thin white-text">49.5Mb</small></h3>
                    </a>
                </span>

            </div>
        </div>
    </span>
</template>

<script>
export default {

    data () {
        return {

        }
    },

}
</script>

<style>
    .download-min {
        min-height: 300px;
    }
    .hover {
        transition: 0.5s;
    }
    .hover:hover {
        background-color: grey;
        color: white;
    }
</style>
