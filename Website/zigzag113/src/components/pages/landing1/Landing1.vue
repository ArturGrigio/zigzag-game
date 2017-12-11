<template>
    <span>
        <div class="section no-padding">

            <div id="#download">
                <carousel></carousel>
            </div>

            <div class="row center">
                <div class="spacer-4"></div>
                <div class="spacer-2"></div>
                <div class="col l4 offset-l4 m6 offset-m3 s12">
                    <iframe id="video" width="100%" src="https://www.youtube.com/embed/yGKe2dzf3jU?rel=0&amp;controls=0&amp;showinfo=0" frameborder="0" gesture="media" allow="encrypted-media" allowfullscreen></iframe>
                </div>
            </div>


            <div class="row center no-margin">
                <div class="spacer-4"></div>
                <div class="spacer-2"></div>
                <h2 class="thin no-margin">
                    <small class="orange-text">PLAY </small>
                </h2>
                <div class="row no-margin">
                    <div class="col l4 offset-l4 m6 offset-m3 s12">
                        <img src="~@/assets/misc/game_logo.png" style="width: 100%"/>
                    </div>
                </div>
            </div>
            <div v-if="beforeLaunch()" class="row center" id="countdown">
                <countdown v-bind:date="countdownEnd"></countdown>
            </div>

            <div class="container" id="creators">
                <creators></creators>
            </div>

            <div class="spacer-4"></div>
            <div class="spacer-2"></div>
            <div class="container" id="overview">
                <overview></overview>
            </div>

            <div class="container" id="lore">
                <lore></lore>
            </div>

            <div class="container" id="requirements">
                <requirements></requirements>
            </div>

            <div class="container" id="binding">
                <bindings></bindings>
            </div>

            <div class="container" id="download">
                <download></download>
            </div>

            <div class="spacer-4"></div>

        </div>
    </span>
</template>

<script>
import countdown from './partials/Countdown.vue';
import carousel from './partials/Carousel.vue';
import requirements from './partials/Requirements.vue';
import overview from './partials/Overview.vue';
import lore from './partials/Lore.vue';
import creators from './partials/Creators.vue';
import bindings from './partials/Binding.vue'
import moment from 'moment';
import download from './partials/Download.vue';

export default {
    components: { countdown, carousel, requirements, overview, lore, creators, bindings, download },

    data () {
        return {
            countdownEnd: '12-12-2017'
        }
    },

    methods: {
        init() {
            $('#video').height($('#video').width() * 0.565)
        },
        beforeLaunch() {
            if(moment(this.countdownEnd, 'MM-DD-YYYY').diff(moment(), 'hours'))
                return true
            return false
        }
    },

    mounted() {
        this.init()
        $(window).resize(this.init)
    }
}
</script>
