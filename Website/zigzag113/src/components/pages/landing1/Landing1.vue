<template>
    <span>
        <div class="section no-padding">

            <div id="#download">
                <carousel></carousel>
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
            <div class="row center" id="countdown">
                <countdown date="12-15-2017"></countdown>
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

export default {
    components: { countdown, carousel, requirements, overview, lore, creators },

  data () {
    return {
      msg: 'Welcome to Your Vue.js App'
    }
  }
}
</script>
