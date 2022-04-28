def on_data_received():
    global cmd
    cmd = serial.read_until(serial.delimiters(Delimiters.HASH))
    basic.show_string(cmd)
    if cmd == "0":
        basic.show_leds("""
            . # # # .
                        . # . # .
                        . # . # .
                        . # . # .
                        . # # # .
        """)
    elif cmd == "1":
        basic.show_leds("""
            . . # . .
                        . # # . .
                        . . # . .
                        . . # . .
                        . # # # .
        """)
    elif cmd == "2":
        basic.show_leds("""
            . # # # .
                        . . . # .
                        . # # # .
                        . # . . .
                        . # # # .
        """)
    elif cmd == "3":
        basic.show_leds("""
            . # # # .
                        . . . # .
                        . # # # .
                        . . . # .
                        . # # # .
        """)
serial.on_data_received(serial.delimiters(Delimiters.HASH), on_data_received)

cmd = ""
isTemp = 1
period = 5

def on_forever():
    global period, isTemp
    if period >= 5:
        period = 0
        if isTemp == 1:
            serial.write_string("!1:TEMP:" + ("" + str(input.temperature())) + "#")
            isTemp = 0
        else:
            serial.write_string("!1:LIGHT:" + ("" + str(input.light_level())) + "#")
            isTemp = 1
    period = period + 1
    basic.pause(1000)
basic.forever(on_forever)
