<template>
    <span>
        <div class="section no-pad-bot">

            <div class="row center no-margin">
                <h1 class="thin"><small class="orange-text">PLAY </small> SHAPELESS <small class="orange-text">IN</small></h1>
            </div>


            <div class="row center" id="countdown">
                <countdown date="12-15-2017"></countdown>
            </div>

            <div id="#download">
                <carousel></carousel>
            </div>

            <div class="spacer-4"></div>
            <div class="spacer-4"></div>
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
import countdown from './landing1/Countdown.vue';
import carousel from './landing1/Carousel.vue';
import requirements from './landing1/Requirements.vue';
import overview from './landing1/Overview.vue';
import lore from './landing1/Lore.vue';

export default {
    components: { countdown, carousel, requirements, overview, lore },

  data () {
    return {
      msg: 'Welcome to Your Vue.js App'
    }
  }
}
</script>
