<template>
    <span>
        <li>
            <a v-bind:href="link.url" class="thin" v-html="link.html || link.text"></a>
        </li>
    </span>
</template>

<script>
    export default {
        props: {
            link: {
                type: Object,
                required: true
            }
        }
    }
</script>
